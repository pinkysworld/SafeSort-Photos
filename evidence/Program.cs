using System.Security.Cryptography;
using System.Text.Json;
using SafeSort.Core;

// Run only against generated fixtures in a fresh temporary directory.
var root = Path.GetFullPath(args[0]);
var output = Path.Combine(root, "website/evidence");
var demo = Path.Combine(OperatingSystem.IsMacOS() ? "/private/tmp" : Path.GetTempPath(), "fotokehr-web-evidence-" + Guid.NewGuid().ToString("N"));
var source = Path.Combine(demo, "Source");
var target = Path.Combine(demo, "Archive");
Directory.CreateDirectory(source);
var names = new[] { "IMG_20230714_120000.jpg", "IMG_20230714_120001.jpg", "IMG_20240502_090000.jpg" };
for (int i = 0; i < names.Length; i++)
    File.Copy(Path.Combine(root, $"src/SafeSort.App/Assets/Demo/demo{(i < 2 ? 0 : 1)}.jpg"), Path.Combine(source, names[i]));
var before = Directory.GetFiles(source).ToDictionary(p => p, p => Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(p))));
var scan = await new Scanner().ScanAsync(new([source]));
var plan = PlanBuilder.Build(scan, new(target, SkipDuplicates: true));
var run = await new Executor().ExecuteAsync(plan, new(RunsDirectory: Path.Combine(demo, "Runs")));
if (run.Cancelled || run.Actions.Count(a => a.Status == ActionStatus.Completed) != 2 || run.Actions.Count(a => a.Status == ActionStatus.Skipped) != 1 || before.Any(p => !File.Exists(p.Key) || Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(p.Key))) != p.Value))
    throw new Exception("Unexpected demo outcome");
foreach (var a in run.Actions.Where(a => a.Status == ActionStatus.Completed))
    if (!File.ReadAllBytes(a.SourcePath).SequenceEqual(File.ReadAllBytes(a.DestinationPath))) throw new Exception("Copy differs");
Directory.CreateDirectory(output);
// Actual generated report: replace only this synthetic run's temporary root.
var csv = File.ReadAllText(Path.Combine(run.ReportDirectory, "report.csv")).Replace(demo, "Demo");
File.WriteAllText(Path.Combine(output, "report.csv"), csv);
File.Copy(Path.Combine(source, names[0]), Path.Combine(output, "mountain.jpg"), true);
File.Copy(Path.Combine(source, names[2]), Path.Combine(output, "mountain-2.jpg"), true);
var evidence = new { generatedAt = DateTimeOffset.UtcNow, sourceCount = scan.Files.Count, duplicateGroups = scan.DuplicateGroups.Count, completed = 2, skipped = 1, originalsUnchanged = true, copiesByteIdentical = true,
    actions = run.Actions.Select(a => new { source = Path.GetRelativePath(demo, a.SourcePath), destination = Path.GetRelativePath(demo, a.DestinationPath), status = a.Status.ToString(), dateSource = a.Date.Source.ToString(), sha256 = a.Hash }) };
File.WriteAllText(Path.Combine(output, "run.json"), JsonSerializer.Serialize(evidence, new JsonSerializerOptions { WriteIndented = true }));
Console.WriteLine(JsonSerializer.Serialize(evidence));
Directory.Delete(demo, true);

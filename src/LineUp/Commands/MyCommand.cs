using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;

namespace LineUp
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MyCommand : BaseCommand<MyCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var docView = await VS.Documents.GetActiveDocumentViewAsync();
            var selection = docView.TextView.Selection.SelectedSpans.FirstOrDefault();

            if (selection != null && selection.Length > 0)
            {
                var snapshot = selection.Snapshot;
                var startLine = snapshot.GetLineFromPosition(selection.Start.Position).LineNumber;
                var endLine = snapshot.GetLineFromPosition(selection.End.Position).LineNumber;

                var lines = new List<string>();

                for (int i = startLine; i <= endLine; i++)
                {
                    var line = snapshot.GetLineFromLineNumber(i).GetText().TrimEnd();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        lines.Add(line);
                    }
                }

                var sortedLines = lines.OrderBy(line => line.Length).ToList();
                var newText = string.Join(Environment.NewLine, sortedLines);

                using (var edit = docView.TextBuffer.CreateEdit())
                {
                    var span = new Span(
                        snapshot.GetLineFromLineNumber(startLine).Start.Position,
                        snapshot.GetLineFromLineNumber(endLine).End.Position
                    );
                    edit.Replace(span, newText);
                    edit.Apply();
                }
            }
        }
    }
}

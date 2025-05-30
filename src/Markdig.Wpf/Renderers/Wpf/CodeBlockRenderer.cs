// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Markdig.Syntax;
using Markdig.Wpf; // This using is necessary for Styles and Commands

namespace Markdig.Renderers.Wpf
{
    public class CodeBlockRenderer : WpfObjectRenderer<CodeBlock>
    {
        protected override void Write(WpfRenderer renderer, CodeBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var button = new Button();
            button.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeBlockStyleKey);

            // Extract code for CommandParameter
            var codeTextBuilder = new StringBuilder();
            if (obj is FencedCodeBlock fencedCodeBlock) // Prefer FencedCodeBlock for structured lines
            {
                var lines = fencedCodeBlock.Lines;
                for (var i = 0; i < lines.Count; i++)
                {
                    codeTextBuilder.AppendLine(lines.Lines[i].Slice.ToString());
                }
            }
            else // Fallback for generic CodeBlock if necessary, though FencedCodeBlock is common
            {
                // Assuming obj.Lines gives access to the raw lines if not a FencedCodeBlock
                // This part might need adjustment based on actual CodeBlock structure if not FencedCodeBlock
                foreach (var line in obj.Lines.Lines)
                {
                     codeTextBuilder.AppendLine(line.Slice.ToString());
                }
            }
            // Remove the last newline character if any
            string codeText = codeTextBuilder.ToString().TrimEnd(Environment.NewLine.ToCharArray());
            button.CommandParameter = codeText;
            button.Command = Commands.CodeExecution;

            // Create TextBlock for Button.Content
            var textBlock = new TextBlock();
            // Use the already built codeText for the TextBlock content
            textBlock.Text = codeText;
            textBlock.FontFamily = new FontFamily("Consolas"); // Ensure monospaced font for code
            // Apply some basic padding or margin if needed, though style might handle this
            textBlock.Margin = new Thickness(2);

            button.Content = textBlock;

            renderer.Push(button);
            // No need to call renderer.WriteLeafRawLines(obj); as content is manually set.
            renderer.Pop();
        }
    }
}

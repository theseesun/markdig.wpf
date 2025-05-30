// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media; // Required for Brushes

using Markdig.Syntax;
using Markdig.Wpf;

namespace Markdig.Renderers.Wpf
{
    public class CodeBlockRenderer : WpfObjectRenderer<CodeBlock>
    {
        protected override void Write(WpfRenderer renderer, CodeBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));

            var grid = new Grid();

            var textBox = new TextBox // Changed from TextBlock
            {
                Text = string.Join(Environment.NewLine, obj.Lines.Lines.Select(l => l.Slice.ToString())),
                IsReadOnly = true,
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                FontFamily = new FontFamily("Consolas"), // Typical for code
                FontSize = 12, // Typical for code
                // To make it behave more like a TextBlock regarding focus and selection:
                IsTabStop = false,
                // AcceptsReturn = true, // Not strictly necessary for display but good for multiline
                // VerticalScrollBarVisibility = ScrollBarVisibility.Auto, // If content might exceed allocated space
                // HorizontalScrollBarVisibility = ScrollBarVisibility.Auto, // If content might exceed allocated space
                TextWrapping = TextWrapping.NoWrap // Usually code is not wrapped, horizontal scroll is preferred
            };
            // Apply specific code font style if available from theme/styles
            textBox.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeStyleKey);


            var button = new Button
            {
                Content = "Execute",
                Command = Commands.CodeExecution,
                FontSize = 10,
                Padding = new Thickness(2),
                Margin = new Thickness(0,0,5,0),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                MinWidth = 0,
                MinHeight = 0,
                IsTabStop = false // Keep button from being a tab stop as well if not desired
            };

            grid.Children.Add(textBox);
            grid.Children.Add(button);

            var blockUIContainer = new BlockUIContainer(grid);
            // Styles.CodeBlockStyleKey is applied to the container.
            // We might need a specific style for the TextBox itself if Styles.CodeStyleKey isn't sufficient
            // or if Styles.CodeBlockStyleKey conflicts.
            blockUIContainer.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeBlockStyleKey);

            renderer.Push(blockUIContainer);
            renderer.Pop();
        }
    }
}

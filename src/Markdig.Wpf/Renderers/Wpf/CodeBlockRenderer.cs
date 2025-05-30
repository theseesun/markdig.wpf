// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls; // Required for StackPanel
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

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
            grid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffd3d3d3"));

            var textBox = new TextBox
            {
                Text = string.Join(Environment.NewLine, obj.Lines.Lines.Select(l => l.Slice.ToString())),
                IsReadOnly = true,
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                FontFamily = new FontFamily("Consolas, Lucida Sans Typewriter, Courier New"),
                Foreground = Brushes.Black,
                FontSize = 12,
                IsTabStop = false,
                TextWrapping = TextWrapping.NoWrap,
                AcceptsReturn = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
            };

            var executeButton = new Button
            {
                Content = "Execute",
                Command = Commands.CodeExecution,
                FontSize = 10,
                Padding = new Thickness(2),
                // Margin = new Thickness(0,0,5,0), // Margin can be on stackpanel or individual buttons
                MinWidth = 0,
                MinHeight = 0,
                IsTabStop = false
            };

            var copyButton = new Button
            {
                Content = "Copy",
                Command = Commands.CopyCode,
                FontSize = 10,
                Padding = new Thickness(2),
                Margin = new Thickness(5,0,0,0), // Add some space between Execute and Copy
                MinWidth = 0,
                MinHeight = 0,
                IsTabStop = false
            };

            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0,0,5,0) // Margin for the panel itself from the corner
            };

            buttonPanel.Children.Add(executeButton);
            buttonPanel.Children.Add(copyButton);

            grid.Children.Add(textBox);
            grid.Children.Add(buttonPanel); // Add the panel containing both buttons

            var blockUIContainer = new BlockUIContainer(grid);

            renderer.Push(blockUIContainer);
            renderer.Pop();
        }
    }
}

// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
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
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            
            var paragraph = new Paragraph();
            paragraph.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeBlockStyleKey);
            
            // Extract code content
            var code = string.Empty;
            if (obj is FencedCodeBlock fencedCodeBlock)
            {
                var lines = fencedCodeBlock.Lines;
                var plainText = new string[lines.Count];
                for (var i = 0; i < lines.Count; i++)
                {
                    plainText[i] = lines.Lines[i].Slice.ToString();
                }
                code = string.Join(Environment.NewLine, plainText);
            }

            var button = new Button
            {
                Content = "Execute",
                Command = Commands.CodeExecution,
                CommandParameter = code
            };

            Grid.SetRow(button, 0);
            Grid.SetRow(paragraph, 1);

            grid.Children.Add(button);
            grid.Children.Add(paragraph);
            
            renderer.Push(grid);
            var previousContainer = renderer.Container;
            renderer.Container = paragraph;
            renderer.WriteLeafRawLines(obj);
            renderer.Container = previousContainer;
            renderer.Pop(); // Pop the grid
        }
    }
}

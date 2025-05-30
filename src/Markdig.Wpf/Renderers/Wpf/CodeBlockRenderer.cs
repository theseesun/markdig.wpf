// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents; // Required for BlockUIContainer
using Markdig.Syntax;
using Markdig.Wpf; // Required for Commands

namespace Markdig.Renderers.Wpf
{
    public class CodeBlockRenderer : WpfObjectRenderer<CodeBlock>
    {
        protected override void Write(WpfRenderer renderer, CodeBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));

            var stackPanel = new StackPanel();
            // Apply style to StackPanel if needed, or let BlockUIContainer handle it.
            // stackPanel.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeBlockStyleKey);

            var textBlock = new TextBlock();
            // Using string.Join to better represent multi-line code
            textBlock.Text = string.Join(Environment.NewLine, obj.Lines.Lines.Select(l => l.Slice.ToString()));


            var button = new Button
            {
                Content = "Execute" // Or any other appropriate text
            };
            button.SetBinding(Button.CommandProperty, new System.Windows.Data.Binding
            {
                Source = Commands.CodeExecution
            });
            // Consider adding CommandParameter if the code text needs to be passed to the command
            // button.CommandParameter = textBlock.Text;

            stackPanel.Children.Add(textBlock);
            stackPanel.Children.Add(button);

            var blockUIContainer = new BlockUIContainer(stackPanel);
            blockUIContainer.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeBlockStyleKey);

            renderer.Push(blockUIContainer);
            renderer.Pop();
        }
    }
}

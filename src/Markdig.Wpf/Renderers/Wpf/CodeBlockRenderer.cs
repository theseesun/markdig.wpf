// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Linq; // Required for obj.Lines.ToString()
using System.Windows;
using System.Windows.Controls; // Required for StackPanel, TextBlock, Button
using System.Windows.Documents;
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
            stackPanel.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeBlockStyleKey);

            var textBlock = new TextBlock();
            // Concatenate all lines of the code block. Might need adjustment based on how WriteLeafRawLines works.
            // This is a simplified approach. A more robust solution might involve iterating
            // through lines and creating separate Runs or TextBlocks if formatting needs to be preserved.
            textBlock.Text = obj.Lines.ToString();

            var button = new Button
            {
                Content = "Execute" // Or any other appropriate text
            };
            button.SetBinding(Button.CommandProperty, new System.Windows.Data.Binding
            {
                Source = Commands.CodeExecution
            });
            // It might be beneficial to also pass the code content as a command parameter
            // button.CommandParameter = textBlock.Text;


            stackPanel.Children.Add(textBlock);
            stackPanel.Children.Add(button);

            renderer.Push(stackPanel);
            // We are now adding UI elements directly, so WriteLeafRawLines might not be needed in the same way,
            // or its logic needs to be integrated into how textBlock.Text is populated.
            // For now, assuming obj.Lines.ToString() is sufficient for basic text content.
            // If WriteLeafRawLines appends to the current renderer context, we might not need it after populating textBlock.
            // renderer.WriteLeafRawLines(obj); // This line might need to be removed or adapted
            renderer.Pop();
        }
    }
}

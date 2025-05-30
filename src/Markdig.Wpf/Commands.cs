// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System.Windows.Input;

namespace Markdig.Wpf
{
    /// <summary>
    /// List of supported commands.
    /// </summary>
    public static class Commands
    {
        /// <summary>
        /// Routed command for Hyperlink.
        /// </summary>
        public static RoutedCommand Hyperlink { get; } = new RoutedCommand(nameof(Hyperlink), typeof(Commands));

        /// <summary>
        /// Routed command for Images.
        /// </summary>
        public static RoutedCommand Image { get; } = new RoutedCommand(nameof(Image), typeof(Commands));

        /// <summary>
        /// Routed command for CodeExecution.
        /// </summary>
        public static RoutedCommand CodeExecution { get; } = new RoutedCommand(nameof(CodeExecution), typeof(Commands));
    }
}

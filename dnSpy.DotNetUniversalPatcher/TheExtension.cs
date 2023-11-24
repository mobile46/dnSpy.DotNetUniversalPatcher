using System.Collections.Generic;
using dnSpy.Contracts.Extension;

namespace dnSpy.DotNetUniversalPatcher {
	[ExportExtension]
	sealed class TheExtension : IExtension {
		public IEnumerable<string> MergedResourceDictionaries {
			get {
				yield break;
			}
		}

		public ExtensionInfo ExtensionInfo => new() {
			ShortDescription = "DotNet Universal Patcher Helper",
		};

		public void OnEvent(ExtensionEvent @event, object? obj) {
		}
	}
}

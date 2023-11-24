using System;
using System.Windows;
using dnlib.DotNet;
using dnSpy.Contracts.Documents.Tabs.DocViewer;
using dnSpy.Contracts.Images;
using dnSpy.Contracts.Menus;

namespace dnSpy.DotNetUniversalPatcher {
	[ExportMenuItem(Icon = DsImagesAttribute.Copy, Group = MenuConstants.GROUP_CTX_SEARCH_TOKENS, Order = -1)]
	sealed class CopyCtxMenuCommand : MenuItemBase {
		public override string GetHeader(IMenuItemContext context) => Constants.Copy;

		public override bool IsVisible(IMenuItemContext context) =>
			context.CreatorObject.Guid == new Guid(MenuConstants.GUIDOBJ_SEARCH_GUID);

		public override bool IsEnabled(IMenuItemContext context) => GetFullName(context) != null;

		public override void Execute(IMenuItemContext context) {
			try {
				Clipboard.SetText(GetFullName(context)!);
			}
			catch {
				// ignored
			}
		}

		static string? GetFullName(IMenuItemContext context) {
			if (context.CreatorObject.Guid != new Guid(MenuConstants.GUIDOBJ_SEARCH_GUID))
				return null;
			var reference = context.Find<TextReference>()?.Reference;
			if (reference is IMemberRef method)
				return method.FullName;
			return reference?.ToString();
		}
	}
}

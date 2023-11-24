using System;
using System.Windows;
using dnlib.DotNet;
using dnSpy.Contracts.Documents.Tabs.DocViewer;
using dnSpy.Contracts.Images;
using dnSpy.Contracts.Menus;

namespace dnSpy.DotNetUniversalPatcher {
	[ExportMenuItem(Icon = DsImagesAttribute.Copy, Group = Constants.CTXMENUGROUP, Order = 0)]
	sealed class CopyFullNameCtxMenuCommand : MenuItemBase {
		public override string GetHeader(IMenuItemContext context) => Constants.CopyFullName;

		public override bool IsVisible(IMenuItemContext context) => context.CreatorObject.Guid ==
		                                                            new Guid(MenuConstants
			                                                            .GUIDOBJ_DOCUMENTVIEWERCONTROL_GUID);

		public override bool IsEnabled(IMenuItemContext context) => GetFullName(context) != null;

		public override void Execute(IMenuItemContext context) {
			try {
				Clipboard.SetText(GetFullName(context)!);
			}
			catch {
				// ignored
			}
		}

		string? GetFullName(IMenuItemContext context) {
			if (context.CreatorObject.Guid != new Guid(MenuConstants.GUIDOBJ_DOCUMENTVIEWERCONTROL_GUID))
				return null;
			var reference = context.Find<TextReference>()?.Reference;
			if (reference is IMemberRef method)
				return method.FullName;
			return null;
		}
	}
}

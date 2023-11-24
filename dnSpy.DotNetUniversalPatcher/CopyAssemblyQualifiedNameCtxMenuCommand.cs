using System;
using System.Windows;
using dnlib.DotNet;
using dnSpy.Contracts.Documents.Tabs.DocViewer;
using dnSpy.Contracts.Images;
using dnSpy.Contracts.Menus;

namespace dnSpy.DotNetUniversalPatcher {
	[ExportMenuItem(Icon = DsImagesAttribute.Copy, Group = Constants.CTXMENUGROUP, Order = 1)]
	sealed class CopyAssemblyQualifiedNameCtxMenuCommand : MenuItemBase {
		public override string GetHeader(IMenuItemContext context) => Constants.CopyAssemblyQualifiedName;

		public override bool IsVisible(IMenuItemContext context) => context.CreatorObject.Guid ==
		                                                            new Guid(MenuConstants
			                                                            .GUIDOBJ_DOCUMENTVIEWERCONTROL_GUID);

		public override bool IsEnabled(IMenuItemContext context) => GetAssemblyQualifiedName(context) != null;

		public override void Execute(IMenuItemContext context) {
			try {
				Clipboard.SetText(GetAssemblyQualifiedName(context)!);
			}
			catch {
				// ignored
			}
		}

		string? GetAssemblyQualifiedName(IMenuItemContext context) {
			if (context.CreatorObject.Guid != new Guid(MenuConstants.GUIDOBJ_DOCUMENTVIEWERCONTROL_GUID))
				return null;
			if (context.Find<TextReference>()?.Reference is IType type)
				return type?.AssemblyQualifiedName;
			return null;
		}
	}
}

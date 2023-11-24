using System;
using System.Linq;
using dnSpy.Contracts.Documents.TreeView;
using dnSpy.Contracts.Menus;
using dnSpy.Contracts.TreeView;

namespace dnSpy.DotNetUniversalPatcher {
	abstract class TVCtxMenuCommand : MenuItemBase<TVContext> {
		protected sealed override object CachedContextKey => ContextKey;
		static readonly object ContextKey = new();

		protected sealed override TVContext? CreateContext(IMenuItemContext context) {
			if (context.CreatorObject.Guid != new Guid(MenuConstants.GUIDOBJ_DOCUMENTS_TREEVIEW_GUID))
				return null;

			var nodes = context.Find<TreeNodeData[]>();
			var newNodes = nodes.OfType<DocumentTreeNodeData>();

			return new TVContext(newNodes);
		}
	}
}

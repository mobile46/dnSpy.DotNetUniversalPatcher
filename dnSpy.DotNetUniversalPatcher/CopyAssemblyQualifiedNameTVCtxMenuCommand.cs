using System.Windows;
using dnSpy.Contracts.Documents.TreeView;
using dnSpy.Contracts.Images;
using dnSpy.Contracts.Menus;

namespace dnSpy.DotNetUniversalPatcher {
	[ExportMenuItem(Icon = DsImagesAttribute.Copy, Group = Constants.TVCTXMENUGROUP, Order = 1)]
	sealed class CopyAssemblyQualifiedNameTVCtxMenuCommand : TVCtxMenuCommand {
		public override string GetHeader(TVContext context) => Constants.CopyAssemblyQualifiedName;

		public override bool IsEnabled(TVContext context) => GetAssemblyQualifiedName(context) != null;

		public override void Execute(TVContext context) {
			try {
				Clipboard.SetText(GetAssemblyQualifiedName(context)!);
			}
			catch {
				// ignored
			}
		}

		string? GetAssemblyQualifiedName(TVContext context) {
			if (context.Nodes.Length == 0)
				return null;
			var node = context.Nodes[0];
			if (node is BaseTypeNode baseTypeNode)
				return baseTypeNode.TypeDefOrRef.AssemblyQualifiedName;
			if (node is DerivedTypeNode derivedTypeNode)
				return derivedTypeNode.TypeDef.AssemblyQualifiedName;
			if (node is TypeNode typeNode)
				return typeNode.TypeDef.AssemblyQualifiedName;
			if (node is TypeReferenceNode typeReferenceNode)
				return typeReferenceNode.TypeRef.AssemblyQualifiedName;
			return null;
		}
	}
}

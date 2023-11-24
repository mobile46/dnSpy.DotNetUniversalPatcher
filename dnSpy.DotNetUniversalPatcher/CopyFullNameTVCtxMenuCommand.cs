using System.Windows;
using dnSpy.Contracts.Documents.TreeView;
using dnSpy.Contracts.Images;
using dnSpy.Contracts.Menus;

namespace dnSpy.DotNetUniversalPatcher {
	[ExportMenuItem(Icon = DsImagesAttribute.Copy, Group = Constants.TVCTXMENUGROUP, Order = 0)]
	sealed class CopyFullNameTVCtxMenuCommand : TVCtxMenuCommand {
		public override string GetHeader(TVContext context) => Constants.CopyFullName;

		public override bool IsEnabled(TVContext context) => GetFullName(context) != null;

		public override void Execute(TVContext context) {
			try {
				Clipboard.SetText(GetFullName(context)!);
			}
			catch {
				// ignored
			}
		}

		string? GetFullName(TVContext context) {
			if (context.Nodes.Length == 0)
				return null;
			var node = context.Nodes[0];
			if (node is AssemblyReferenceNode assemblyReferenceNode)
				return assemblyReferenceNode.AssemblyRef.FullName;
			if (node is ModuleReferenceNode moduleReferenceNode)
				return moduleReferenceNode.ModuleRef.FullName;
			if (node is BaseTypeNode baseTypeNodeB)
				return baseTypeNodeB.TypeDefOrRef.FullName;
			if (node is DerivedTypeNode derivedTypeNode)
				return derivedTypeNode.TypeDef.FullName;
			if (node is TypeNode typeNode)
				return typeNode.TypeDef.FullName;
			if (node is TypeReferenceNode typeReferenceNode)
				return typeReferenceNode.TypeRef.FullName;
			if (node is EventNode eventNode)
				return eventNode.EventDef.FullName;
			if (node is EventReferenceNode eventReferenceNode)
				return eventReferenceNode.EventRef.FullName;
			if (node is FieldNode fieldNode)
				return fieldNode.FieldDef.FullName;
			if (node is FieldReferenceNode fieldReferenceNode)
				return fieldReferenceNode.FieldRef.FullName;
			if (node is MethodNode methodNode)
				return methodNode.MethodDef.FullName;
			if (node is MethodReferenceNode methodReferenceNode)
				return methodReferenceNode.MethodRef.FullName;
			if (node is PropertyNode propertyNode)
				return propertyNode.PropertyDef.FullName;
			if (node is PropertyReferenceNode propertyReferenceNode)
				return propertyReferenceNode.PropertyRef.FullName;
			if (node is NamespaceNode namespaceNode)
				return namespaceNode.Name;
			return node.ToString();
		}
	}
}

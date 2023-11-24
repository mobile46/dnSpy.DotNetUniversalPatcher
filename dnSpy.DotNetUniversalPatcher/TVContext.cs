using System.Collections.Generic;
using System.Linq;
using dnSpy.Contracts.Documents.TreeView;

namespace dnSpy.DotNetUniversalPatcher {
	sealed class TVContext {
		internal DocumentTreeNodeData[] Nodes { get; }

		public TVContext(IEnumerable<DocumentTreeNodeData> nodes) => Nodes = nodes.ToArray();
	}
}

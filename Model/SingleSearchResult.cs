using Data.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQCN.Model {
	public class SingleSearchResult {
		public Post post;

		public SingleSearchResult(Post post) {
			this.post = post;
		}
	}
}

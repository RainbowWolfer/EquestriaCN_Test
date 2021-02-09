using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace EQCN {
	public sealed class AdjustableSize: IAdjustSize {
		public const int BoundaryWidth = 800;
		private readonly Action<double, double> narrowAction;
		private readonly Action<double, double> wideAction;

		public AdjustableSize(Action<double, double> narrowAction, Action<double, double> wideAction) {
			this.narrowAction = narrowAction;
			this.wideAction = wideAction;
		}

		public void InitializeSize(double height, double width) {
			if(width < BoundaryWidth) {
				narrowAction(height, width);
			} else {
				wideAction(height, width);
			}
		}
		public void AdjustSize(double newHeight, double newWidth, double oldHeight, double oldWidth) {
			if(newWidth <= BoundaryWidth && oldWidth >= BoundaryWidth) {//narrow
				narrowAction(newHeight, newWidth);
			} else if(newWidth >= BoundaryWidth && oldWidth <= BoundaryWidth) {//wide
				wideAction(newHeight, newWidth);
			}
		}
		public void AdjustSize(Size newSize, Size oldSize) {
			AdjustSize(newSize.Height, newSize.Width, oldSize.Height, oldSize.Width);
		}
	}
}

/*************************************************************************************************************************\

Author:		Mircea-Cristian Racasan <darx_kies@gmx.net>
Copyright: 	2005 by Mircea-Cristian Racasan

This program is free software; you can redistribute it and/or modify it under the terms of the 
GNU General Public License as published by the Free Software Foundation; either version 2 of the License, 
or (at your option) any later version.

This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with this program; 
if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA

\*************************************************************************************************************************/

using System;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;

namespace Ntx.GD
{
	[StructLayout( LayoutKind.Sequential )]
	public class Point
	{
		private int x, y;
		
		public Point()
		{
			this.X = 0;
			this.Y = 0;
		}
		
		public Point( int x, int y )
		{
			this.X = x;
			this.Y = y;
		}
		
		public int X
		{
			get { return x; }
			set { x = value; }
		}
		
		public int Y
		{
			get { return y; }
			set { y = value; }
		}
		
		internal static int[] GetIntArray( ArrayList list )
		{
			// TODO is there another way to solve it?
			// Maybe take a Point[] and do some Marshal type memcpy? - Kevin
			int[] intList = new int[ list.Count * 2 ];
			for( int i = 0; i < list.Count; i++ )
			{
				Point point = (Point) list[i];
				intList[ i * 2 + 0 ] = point.X;
				intList[ i * 2 + 1 ] = point.Y;
			}
			
			return intList;
		}
	}
}

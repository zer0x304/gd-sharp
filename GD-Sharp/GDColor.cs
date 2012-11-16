/*************************************************************************************************************************\

Author:		Kevin Tam <kevin@glorat.net>
Copyright: 	2005 by Kevin Tam

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

namespace Ntx.GD
{
	public struct GDColor
	{
		public static GDColor UNKNOWN = new GDColor(-1);

		public int Index;
		internal GDColor(int i)
		{
			Index = i;
		}

		/// <summary>
		/// Convert GDColor to int for native GD API calls
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public static implicit operator int(GDColor i)
		{
			return i.Index;
		}

		
		/// <summary>
		/// Convert int to GDColor for exposing from this API
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public static explicit operator GDColor(int i)
		{
			GDColor c;
			c.Index = i;
			return c;
		}
	}
}

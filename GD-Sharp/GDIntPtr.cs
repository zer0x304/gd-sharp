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
	/// <summary>
	/// Some GD functions return void* that need to be freed using gdFree.
	/// </summary>
	public class GDIntPtr : IDisposable
	{
		IntPtr mPtr;
		public GDIntPtr(IntPtr ptr)
		{
			mPtr = ptr;
		}

		~GDIntPtr()
		{
			Free();
		}

		public bool IsNull
		{
			get {return mPtr == IntPtr.Zero;}
		}

		public void Free()
		{
			if (!IsNull)
			{
				GDImport.gdFree(mPtr);
				mPtr = IntPtr.Zero;
			}
		}

		public void Dispose()
		{
			Free();
			GC.SuppressFinalize(this);
		
		}

		public static implicit operator GDIntPtr(IntPtr ptr)
		{
			return new GDIntPtr(ptr);
		}

		public static implicit operator IntPtr(GDIntPtr gdptr)
		{
			return gdptr.mPtr;
		}
	}
}

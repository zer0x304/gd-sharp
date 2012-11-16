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
using Ntx.GD;

namespace Ntx.GD
{
	public class Font: IDisposable
	{
		public enum Type: int
		{
			Small = 0,
			Large,
			MediumBold,
			Giant,
			Tiny
		}	
		
		[StructLayout( LayoutKind.Sequential )]
		private struct GDFont
		{
			public int 	nchars;
			public int 	offset;
			public int 	w;
			public int 	h;
		}
		
		private HandleRef 	handle;
		private bool 		disposed = false;
		
		public Font( Type font )
		{
			IntPtr fontHandle;
			
			switch( font )
			{
				case Type.Small:
					fontHandle = GDImport.gdFontGetSmall();
					break;
				case Type.Large:
					fontHandle = GDImport.gdFontGetLarge();
					break;
				case Type.MediumBold:
					fontHandle = GDImport.gdFontGetMediumBold();
					break;
				case Type.Giant:
					fontHandle = GDImport.gdFontGetGiant();
					break;
				case Type.Tiny:
					fontHandle = GDImport.gdFontGetTiny();
					break;
				default:
					throw new ApplicationException( font + " is no valid font." );
			}
			
			if( fontHandle == IntPtr.Zero )
				throw new ApplicationException( "The font retrieval failed." );
				
			this.handle = new HandleRef( this, fontHandle );
		}
				
		~Font()
	   	{
	   		Dispose( false );
	   	}
	
		public void Dispose()
		{
	   		Dispose( true );
	      		GC.SuppressFinalize(this); 
		}   	
		
		protected virtual void Dispose( bool managed )
		{
			if( !this.disposed )
	   			this.disposed = true;
	   	}
		
		public void CheckDisposed()
		{
			if( this.disposed )
				throw new ApplicationException( "Font has been disposed already." );
		}
		
		internal HandleRef GetHandle()
		{
			CheckDisposed();
				
			return this.handle;
		}
		
		public int Width()
		{
			CheckDisposed();
				
			return ( (GDFont) Marshal.PtrToStructure( this.handle.Handle, typeof( GDFont ) ) ).w;		
		}
		
		public int Height()
		{
			CheckDisposed();
				
			return ( (GDFont) Marshal.PtrToStructure( this.handle.Handle, typeof( GDFont ) ) ).h;		
		}
		
		public int NChars()
		{
			CheckDisposed();
				
			return ( (GDFont) Marshal.PtrToStructure( this.handle.Handle, typeof( GDFont ) ) ).nchars;		
		}
		
		public int Offset()
		{
			CheckDisposed();
				
			return ( (GDFont) Marshal.PtrToStructure( this.handle.Handle, typeof( GDFont ) ) ).offset;		
		}
	}

}

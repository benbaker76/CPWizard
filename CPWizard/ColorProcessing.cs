// Matrix Operations for Image Processing
// Converted from C to C# By Ben Baker
// Thanks to Paul Haeberli
// http://www.sgi.com/misc/grafica/matrix/

using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace CPWizard
{
	public class ColorProcessing
	{
		private const float RLUM = (0.3086f);
		private const float GLUM = (0.6094f);
		private const float BLUM = (0.0820f);
		private const int OFFSET_R = 3;
		private const int OFFSET_G = 2;
		private const int OFFSET_B = 1;
		private const int OFFSET_A = 0;

		// MultiplyMatrix -	multiply two matricies
		public static void MultiplyMatrix(ref ColorMatrix a, ref ColorMatrix b, ref ColorMatrix c)
		{
			int x, y;
			ColorMatrix temp = new ColorMatrix();

			for (y = 0; y < 5; y++)
				for (x = 0; x < 5; x++)
					temp[y, x] = b[y, 0] * a[0, x]
						+ b[y, 1] * a[1, x]
						+ b[y, 2] * a[2, x]
						+ b[y, 3] * a[3, x]
						+ b[y, 4] * a[4, x];

			for (y = 0; y < 5; y++)
				for (x = 0; x < 5; x++)
					c[y, x] = temp[y, x];
		}

		// CreateIdentityMatrix - make an identity matrix
		public static void CreateIdentityMatrix(ref ColorMatrix matrix)
		{
			matrix = new ColorMatrix(new float[][]
								{
									new float[] {1, 0, 0, 0, 0},
									new float[] {0, 1, 0, 0, 0},
									new float[] {0, 0, 1, 0, 0},
									new float[] {0, 0, 0, 1, 0},
									new float[] {0, 0, 0, 0, 1}});
		}

		// Transform3dPointMatrix - transform a 3D point using a matrix
		public static void Transform3dPointMatrix(ref ColorMatrix matrix, float x, float y, float z, ref float tx, ref float ty, ref float tz)
		{
			tx = x * matrix[0, 0] + y * matrix[1, 0] + z * matrix[2, 0] + matrix[3, 0];
			ty = x * matrix[0, 1] + y * matrix[1, 1] + z * matrix[2, 1] + matrix[3, 1];
			tz = x * matrix[0, 2] + y * matrix[1, 2] + z * matrix[2, 2] + matrix[3, 2];
		}

		// ColourScaleMatrix - make a color scale matrix
		public static void ColourScaleMatrix(ref ColorMatrix mat, float rscale, float gscale, float bscale)
		{
			ColorMatrix nmat = new ColorMatrix(new float[][]
											{
												new float[] {rscale, 0, 0, 0, 0},
												new float[] {0, gscale, 0, 0, 0},
												new float[] {0, 0, bscale, 0, 0},
												new float[] {0, 0, 0, 1, 0},
												new float[] {0, 0, 0, 0, 1}});
			MultiplyMatrix(ref nmat, ref mat, ref mat);
		}

		// LuminanceMatrix - make a luminance matrix
		public static void LuminanceMatrix(ref ColorMatrix mat)
		{
			float rwgt, gwgt, bwgt;
			rwgt = RLUM;
			gwgt = GLUM;
			bwgt = BLUM;
			ColorMatrix mmat = new ColorMatrix(new float[][]
							{
								new float[] {rwgt, rwgt, rwgt, 0, 0},
								new float[] {gwgt, gwgt, gwgt, 0, 0},
								new float[] {bwgt, bwgt, bwgt, 0, 0},
								new float[] {0, 0, 0, 1, 0},
								new float[] {0, 0, 0, 0, 1}});
			MultiplyMatrix(ref mmat, ref mat, ref mat);
		}

		// SaturateMatrix - make a saturation matrix
		public static void SaturateMatrix(ref ColorMatrix mat, float sat)
		{
			float a, b, c, d, e, f, g, h, i;
			float rwgt, gwgt, bwgt;
			rwgt = RLUM;
			gwgt = GLUM;
			bwgt = BLUM;
			a = (float)((1.0f - sat) * rwgt + sat);
			b = (float)((1.0f - sat) * rwgt);
			c = (float)((1.0f - sat) * rwgt);
			d = (float)((1.0f - sat) * gwgt);
			e = (float)((1.0f - sat) * gwgt + sat);
			f = (float)((1.0f - sat) * gwgt);
			g = (float)((1.0f - sat) * bwgt);
			h = (float)((1.0f - sat) * bwgt);
			i = (float)((1.0f - sat) * bwgt + sat);
			ColorMatrix mmat = new ColorMatrix(new float[][]
							{
								new float[] {a, b, c, 0, 0},
								new float[] {d, e, f, 0, 0},
								new float[] {g, h, i, 0, 0},
								new float[] {0, 0, 0, 1, 0},
								new float[] {0, 0, 0, 0, 1}});
			MultiplyMatrix(ref mmat, ref mat, ref mat);
		}

		// OffsetMatrix - offset r, g, and b
		public static void OffsetMatrix(ref ColorMatrix mat, float roffset, float goffset, float boffset)
		{
			ColorMatrix mmat = new ColorMatrix(new float[][]
							{
								new float[] {1, 0, 0, 0, 0},
								new float[] {0, 1, 0, 0, 0},
								new float[] {0, 0, 1, 0, 0},
								new float[] {roffset, goffset, boffset, 1, 0},
								new float[] {0, 0, 0, 0, 1}});
			MultiplyMatrix(ref mmat, ref mat, ref mat);
		}

		// XRotateMatrix - rotate about the x (red) axis
		public static void XRotateMatrix(ref ColorMatrix mat, float rs, float rc)
		{
			ColorMatrix mmat = new ColorMatrix(new float[][]
							{
								new float[] {1, 0, 0, 0, 0},
								new float[] {0, rc, rs, 0, 0},
								new float[] {0, -rs, rc, 0, 0},
								new float[] {0, 0, 0, 1, 0},
								new float[] {0, 0, 0, 0, 1}});
			MultiplyMatrix(ref mmat, ref mat, ref mat);
		}

		// YRotateMatrix - rotate about the y (green) axis
		public static void YRotateMatrix(ref ColorMatrix mat, float rs, float rc)
		{
			ColorMatrix mmat = new ColorMatrix(new float[][]
							{
								new float[] {rc, 0, -rs, 0, 0},
								new float[] {0, 1, 0, 0, 0},
								new float[] {rs, 0, rc, 0, 0},
								new float[] {0, 0, 0, 1, 0},
								new float[] {0, 0, 0, 0, 1}});
			MultiplyMatrix(ref mmat, ref mat, ref mat);
		}

		// ZRotateMatrix - rotate about the z (blue) axis
		public static void ZRotateMatrix(ref ColorMatrix mat, float rs, float rc)
		{
			ColorMatrix mmat = new ColorMatrix(new float[][]
							{
								new float[] {rc, rs, 0, 0, 0},
								new float[] {-rs, rc, 0, 0, 0},
								new float[] {0, 0, 1, 0, 0},
								new float[] {0, 0, 0, 1, 0},
								new float[] {0, 0, 0, 0, 1}});
			MultiplyMatrix(ref mmat, ref mat, ref mat);
		}

		// ZShearMatrix - shear z using x and y.
		public static void ZShearMatrix(ref ColorMatrix mat, float dx, float dy)
		{
			ColorMatrix mmat = new ColorMatrix(new float[][]
							{
								new float[] {1, 0, dx, 0, 0},
								new float[] {0, 1, dy, 0, 0},
								new float[] {0, 0, 1, 0, 0},
								new float[] {0, 0, 0, 1, 0},
								new float[] {0, 0, 0, 0, 1}});
			MultiplyMatrix(ref mmat, ref mat, ref mat);
		}

		// HueRotateMatrix - rotate the hue, while maintaining luminance.
		public static void HueRotateMatrix(ref ColorMatrix mat, float rot)
		{
			ColorMatrix mmat = null;
			float mag;
			float lx = 0, ly = 0, lz = 0;
			float xrs, xrc;
			float yrs, yrc;
			float zrs, zrc;
			float zsx, zsy;

			CreateIdentityMatrix(ref mmat);

			// rotate the grey vector into positive Z
			mag = (float)(Math.Sqrt(2.0));
			xrs = (float)(1.0f / mag);
			xrc = (float)(1.0f / mag);
			XRotateMatrix(ref mmat, xrs, xrc);
			mag = (float)(Math.Sqrt(3.0f));
			yrs = (float)(-1.0f / mag);
			yrc = (float)(Math.Sqrt(2.0f) / mag);
			YRotateMatrix(ref mmat, yrs, yrc);

			// shear the space to make the luminance plane horizontal
			Transform3dPointMatrix(ref mmat, RLUM, GLUM, BLUM, ref lx, ref ly, ref lz);
			zsx = lx / lz;
			zsy = ly / lz;
			ZShearMatrix(ref mmat, zsx, zsy);

			// rotate the hue
			zrs = (float)(Math.Sin(rot * Math.PI / 180.0f));
			zrc = (float)(Math.Cos(rot * Math.PI / 180.0f));
			ZRotateMatrix(ref mmat, zrs, zrc);

			// unshear the space to put the luminance plane back
			ZShearMatrix(ref mmat, -zsx, -zsy);

			// rotate the grey vector back into place
			YRotateMatrix(ref mmat, -yrs, yrc);
			XRotateMatrix(ref mmat, -xrs, xrc);

			MultiplyMatrix(ref mmat, ref mat, ref mat);
		}

		public static ColorMatrix Multiply(ColorMatrix f1, ColorMatrix f2)
		{
			ColorMatrix X = new ColorMatrix();

			int size = 5;

			float[] column = new float[5];

			for (int j = 0; j < 5; j++)
			{
				for (int k = 0; k < 5; k++)
					column[k] = f1[k, j];

				for (int i = 0; i < 5; i++)
				{
					float s = 0;

					for (int k = 0; k < size; k++)
						s += f2[i, k] * column[k];

					X[i, j] = s;
				}
			}

			return X;
		}

		public static Bitmap DrawImageWithMatrix(Bitmap b, ColorMatrix cm)
		{
			try
			{
				Bitmap bmp = new Bitmap(b);

				using (Graphics g = Graphics.FromImage(bmp))
				{
					ImageAttributes imgattr = new ImageAttributes();
					Rectangle rc = new Rectangle(0, 0, b.Width, b.Height);

					// associate the ColorMatrix object with an ImageAttributes object
					imgattr.SetColorMatrix(cm);

					// draw the copy of the source image back over the original image, 
					// applying the ColorMatrix
					g.DrawImage(bmp, rc, 0, 0, b.Width, b.Height, GraphicsUnit.Pixel, imgattr);
				}

				return bmp;
			}
			catch (Exception ex)
			{
				LogFile.WriteLine("DrawImageWithMatrix", "ColourProcessing", ex.Message, ex.StackTrace);
			}

			return null;
		}
	}
}
#ifndef POSTFUNCS_INCLUDE
#define POSTFUNCS_INCLUDE

#include "UnityCG.cginc"

float KERNEL_SHARPEN[9] = {
	0.0, -1.0, 0.0,
	-1.0, 5.0, -1.0,
	0.0, -1.0, 0.0
};
float KERNEL_SHARPEN2[9] = {
	-1.0, -1.0, -1.0,
	-1.0, 9.0,  -1.0,
	-1.0, -1.0, -1.0
};

float KERNEL_EDGEDETECT[9] = {
	1.0, 1.0, 1.0,
	1.0, -8.0, 1.0,
	1.0, 1.0, 1.0
};

float KERNEL_GBLUR[9] = {
	1.0 / 16.0, 2.0 / 16.0, 1.0 / 16.0,
	2.0 / 16.0, 4.0 / 16.0, 2.0 / 16.0,
	1.0 / 16.0, 2.0 / 16.0, 1.0 / 16.0
};

float KERNEL_GBLUR5X5[25] = {
	2.0 / 159.0,	4.0 / 159.0,	5.0 / 159.0,	4.0 / 159.0,	2.0 / 159.0,
	4.0 / 159.0,	9.0 / 159.0,	12.0 / 159.0,	9.0 / 159.0,	4.0 / 159.0,
	5.0 / 159.0,	12.0 / 159.0,	15.0 / 159.0,	12.0 / 159.0,	5.0 / 159.0,
	4.0 / 159.0,	9.0 / 159.0,	12.0 / 159.0,	9.0 / 159.0,	4.0 / 159.0,
	2.0 / 159.0,	4.0 / 159.0,	5.0 / 159.0,	4.0 / 159.0,	2.0 / 159.0
};

inline fixed3 ApplyKernel3x3(float kernel[9], fixed3 textures[9]) // function for applying basic kernels
{
	fixed3 col = fixed3(0.0, 0.0, 0.0);
	for (int j = 0; j < 9; j++)
	{
		col += textures[j] * (kernel[j]);
	}
	return col;
}

inline fixed3 ApplyKernel5x5(float kernel[25], fixed3 textures[25]) // function for applying basic kernels, 5x5
{
	fixed3 col = fixed3(0.0, 0.0, 0.0);
	for (int j = 0; j < 25; j++)
	{
		col += textures[j] * (kernel[j]);
	}
	return col;
}

// 5x5 to 3x3
inline void Get3x3From5x5(fixed3 tex[25], int index, out fixed3 result[9])
{
	fixed3 a[9] = { tex[index - 6], tex[index - 5], tex[index - 4],
		tex[index - 1], tex[index], tex[index + 1],
		tex[index + 4], tex[index + 5], tex[index + 6] };
	result = a;
}

// GRAYSCALE
inline fixed3 Grayscale(fixed3 frag)
{
	float average = 0.2126 * frag.r + 0.7152 * frag.g + 0.0722 * frag.b;
	return fixed3(average, average, average);
}

// SOBEL CALCULATION 
inline fixed3 ApplySobel(fixed3 textures[9])
{
	float3x3 KERNEL_SOBELX = float3x3(
		-1.0, 0.0, 1.0,
		-2.0, 0.0, 2.0,
		-1.0, 0.0, 1.0
		);
	float3x3 KERNEL_SOBELY = float3x3(
		1.0, 2.0, 1.0,
		0.0, 0.0, 0.0,
		-1.0, -2.0, -1.0
		);

	float mag = 0.0;

	float mGx = 0.0;
	float mGy = 0.0;

	for (int i = 0, k = 0; i < 3; i++)
	{
		for (int j = 0; j < 3; j++, k++)
		{
			float Gx = KERNEL_SOBELX[i][j] * Grayscale(textures[k]);
			float Gy = KERNEL_SOBELY[i][j] * Grayscale(textures[k]);

			mGx += Gx;
			mGy += Gy;
		}
	}

	mag = sqrt(pow(mGx, 2) + pow(mGy, 2));

	return (fixed3(mag, mag, mag)) * 1.5;

}

#endif
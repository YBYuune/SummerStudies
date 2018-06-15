#ifndef POSTFUNCS_INCLUDE
#define POSTFUNCS_INCLUDE

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
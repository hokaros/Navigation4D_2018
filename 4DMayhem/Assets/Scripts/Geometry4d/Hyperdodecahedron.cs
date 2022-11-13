using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Hyperdodecahedron : Polytope4Accelerated
{
	[SerializeField] float edgeLength = 2f;
	public override List<Vector4> StartVertices
	{
		get
		{
			float sqrt5 = Mathf.Sqrt(5f);
			float phi = (1 + sqrt5) / 2;
			float phisq = phi * phi;

			List<Vector4> vertices = new List<Vector4>();
			Helpers.CollectSignedPermutations(vertices, new Vector4(0, 0, 2 * edgeLength, 2 * edgeLength));
			Helpers.CollectSignedPermutations(vertices, new Vector4(edgeLength, edgeLength, edgeLength, sqrt5 * edgeLength));
			Helpers.CollectSignedPermutations(vertices, new Vector4(edgeLength / phisq, edgeLength * phi, edgeLength * phi, edgeLength * phi));
			Helpers.CollectSignedPermutations(vertices, new Vector4(edgeLength / phi, edgeLength / phi, edgeLength / phi, edgeLength * phisq));
			Helpers.CollectSignedPermutations(vertices, new Vector4(0, edgeLength / phisq, edgeLength, edgeLength * phisq), true);
			Helpers.CollectSignedPermutations(vertices, new Vector4(0, edgeLength / phi, edgeLength * phi, edgeLength * sqrt5), true);
			Helpers.CollectSignedPermutations(vertices, new Vector4(edgeLength / phi, edgeLength, edgeLength * phi, 2 * edgeLength), true);

            StreamWriter writer = new StreamWriter("assets/polytopes/hiperdodecahedronVertices.txt");
            for (int i = 0; i < vertices.Count; i++)
            {
                writer.Write(vertices[i].x.ToString().Replace(",", ".") + "," + vertices[i].y.ToString().Replace(",", ".") + "," + vertices[i].z.ToString().Replace(",", ".") + "," + vertices[i].w.ToString().Replace(",", ".") + ";");
            }
            writer.Close();
            return vertices;
		}
	}

	public override List<Edge> Edges
	{
		get
		{
			List<Edge> edges = new List<Edge>();
			List<Vector4> vertices = StartVertices;
			for (int i = 0; i < vertices.Count - 1; i++)
			{
				for (int j = i + 1; j < vertices.Count; j++)
				{
					if (Mathf.Abs(Vector4.Distance(vertices[i], vertices[j])) - edgeLength < 0.1f)
					{
						edges.Add(new Edge(i, j));
					}
				}
			}
			//StreamWriter writer = new StreamWriter("assets/polytopes/hiperdodecahedronEdges.txt");
			//for (int i = 0; i < edges.Count; i++)
			//{
			//	writer.Write(edges[i].Item1 + "," + edges[i].Item2 + ";");
			//}
			//writer.Close();
			return edges;
		}
	}

	/// <summary>
	/// returns vertices of a cycle if provided edges form a cycle IN GIVEN ORDER
	/// </summary>
	/// <param name="edges"></param>
	/// <returns></returns>
	public List<int> Cycle(List<Edge> edges)
    {
		List<int> cycle = new List<int>() { edges[0].startId };
		for(int i = 1; i < edges.Count; i++)
        {
			if(edges[i].startId == cycle[cycle.Count - 1])
            {
				cycle.Add(edges[i].endId);
			}
			else if(edges[i].endId == cycle[cycle.Count - 1])
            {
				cycle.Add(edges[i].startId);
			}
            else
            {
				return new List<int>();
            }
        }
		if (edges[0].endId == cycle[cycle.Count - 1])
		{
			return cycle;
		}
		else
		{
			return new List<int>();
		}
    }

	public override List<List<int>> Faces
	{
		get
		{
			List<List<int>> faces = new List<List<int>>();
			faces.Add(new List<int>() { 0, 312, 216, 220, 316 });
			faces.Add(new List<int>() { 0, 312, 408, 456, 336 });
			faces.Add(new List<int>() { 0, 312, 432, 480, 348 });
			faces.Add(new List<int>() { 0, 316, 412, 460, 336 });
			faces.Add(new List<int>() { 0, 316, 436, 484, 348 });
			faces.Add(new List<int>() { 0, 336, 240, 252, 348 });
			faces.Add(new List<int>() { 1, 313, 217, 221, 317 });
			faces.Add(new List<int>() { 1, 313, 409, 457, 337 });
			faces.Add(new List<int>() { 1, 313, 433, 481, 349 });
			faces.Add(new List<int>() { 1, 317, 413, 461, 337 });
			faces.Add(new List<int>() { 1, 317, 437, 485, 349 });
			faces.Add(new List<int>() { 1, 337, 241, 253, 349 });
			faces.Add(new List<int>() { 2, 314, 218, 222, 318 });
			faces.Add(new List<int>() { 2, 314, 410, 458, 338 });
			faces.Add(new List<int>() { 2, 314, 434, 482, 350 });
			faces.Add(new List<int>() { 2, 318, 414, 462, 338 });
			faces.Add(new List<int>() { 2, 318, 438, 486, 350 });
			faces.Add(new List<int>() { 2, 338, 242, 254, 350 });
			faces.Add(new List<int>() { 3, 315, 219, 223, 319 });
			faces.Add(new List<int>() { 3, 315, 411, 459, 339 });
			faces.Add(new List<int>() { 3, 315, 435, 483, 351 });
			faces.Add(new List<int>() { 3, 319, 415, 463, 339 });
			faces.Add(new List<int>() { 3, 319, 439, 487, 351 });
			faces.Add(new List<int>() { 3, 339, 243, 255, 351 });
			faces.Add(new List<int>() { 4, 328, 232, 234, 330 });
			faces.Add(new List<int>() { 4, 328, 424, 464, 340 });
			faces.Add(new List<int>() { 4, 328, 448, 488, 352 });
			faces.Add(new List<int>() { 4, 330, 426, 466, 340 });
			faces.Add(new List<int>() { 4, 330, 450, 490, 352 });
			faces.Add(new List<int>() { 4, 340, 244, 256, 352 });
			faces.Add(new List<int>() { 5, 329, 233, 235, 331 });
			faces.Add(new List<int>() { 5, 329, 425, 465, 341 });
			faces.Add(new List<int>() { 5, 329, 449, 489, 353 });
			faces.Add(new List<int>() { 5, 331, 427, 467, 341 });
			faces.Add(new List<int>() { 5, 331, 451, 491, 353 });
			faces.Add(new List<int>() { 5, 341, 245, 257, 353 });
			faces.Add(new List<int>() { 6, 320, 224, 225, 321 });
			faces.Add(new List<int>() { 6, 320, 416, 472, 344 });
			faces.Add(new List<int>() { 6, 320, 440, 496, 356 });
			faces.Add(new List<int>() { 6, 321, 417, 473, 344 });
			faces.Add(new List<int>() { 6, 321, 441, 497, 356 });
			faces.Add(new List<int>() { 6, 344, 248, 260, 356 });
			faces.Add(new List<int>() { 7, 322, 226, 227, 323 });
			faces.Add(new List<int>() { 7, 322, 418, 474, 345 });
			faces.Add(new List<int>() { 7, 322, 442, 498, 357 });
			faces.Add(new List<int>() { 7, 323, 419, 475, 345 });
			faces.Add(new List<int>() { 7, 323, 443, 499, 357 });
			faces.Add(new List<int>() { 7, 345, 249, 261, 357 });
			faces.Add(new List<int>() { 8, 332, 236, 238, 334 });
			faces.Add(new List<int>() { 8, 332, 428, 468, 342 });
			faces.Add(new List<int>() { 8, 332, 452, 492, 354 });
			faces.Add(new List<int>() { 8, 334, 430, 470, 342 });
			faces.Add(new List<int>() { 8, 334, 454, 494, 354 });
			faces.Add(new List<int>() { 8, 342, 246, 258, 354 });
			faces.Add(new List<int>() { 9, 333, 237, 239, 335 });
			faces.Add(new List<int>() { 9, 333, 429, 469, 343 });
			faces.Add(new List<int>() { 9, 333, 453, 493, 355 });
			faces.Add(new List<int>() { 9, 335, 431, 471, 343 });
			faces.Add(new List<int>() { 9, 335, 455, 495, 355 });
			faces.Add(new List<int>() { 9, 343, 247, 259, 355 });
			faces.Add(new List<int>() { 10, 324, 228, 229, 325 });
			faces.Add(new List<int>() { 10, 324, 420, 476, 346 });
			faces.Add(new List<int>() { 10, 324, 444, 500, 358 });
			faces.Add(new List<int>() { 10, 325, 421, 477, 346 });
			faces.Add(new List<int>() { 10, 325, 445, 501, 358 });
			faces.Add(new List<int>() { 10, 346, 250, 262, 358 });
			faces.Add(new List<int>() { 11, 326, 230, 231, 327 });
			faces.Add(new List<int>() { 11, 326, 422, 478, 347 });
			faces.Add(new List<int>() { 11, 326, 446, 502, 359 });
			faces.Add(new List<int>() { 11, 327, 423, 479, 347 });
			faces.Add(new List<int>() { 11, 327, 447, 503, 359 });
			faces.Add(new List<int>() { 11, 347, 251, 263, 359 });
			faces.Add(new List<int>() { 12, 360, 264, 266, 362 });
			faces.Add(new List<int>() { 12, 360, 504, 560, 388 });
			faces.Add(new List<int>() { 12, 360, 508, 564, 390 });
			faces.Add(new List<int>() { 12, 362, 506, 562, 388 });
			faces.Add(new List<int>() { 12, 362, 510, 566, 390 });
			faces.Add(new List<int>() { 12, 388, 292, 294, 390 });
			faces.Add(new List<int>() { 13, 361, 265, 267, 363 });
			faces.Add(new List<int>() { 13, 361, 505, 561, 389 });
			faces.Add(new List<int>() { 13, 361, 509, 565, 391 });
			faces.Add(new List<int>() { 13, 363, 507, 563, 389 });
			faces.Add(new List<int>() { 13, 363, 511, 567, 391 });
			faces.Add(new List<int>() { 13, 389, 293, 295, 391 });
			faces.Add(new List<int>() { 14, 364, 268, 270, 366 });
			faces.Add(new List<int>() { 14, 364, 512, 552, 384 });
			faces.Add(new List<int>() { 14, 364, 513, 553, 385 });
			faces.Add(new List<int>() { 14, 366, 516, 556, 384 });
			faces.Add(new List<int>() { 14, 366, 517, 557, 385 });
			faces.Add(new List<int>() { 14, 384, 288, 289, 385 });
			faces.Add(new List<int>() { 15, 365, 269, 271, 367 });
			faces.Add(new List<int>() { 15, 365, 514, 554, 386 });
			faces.Add(new List<int>() { 15, 365, 515, 555, 387 });
			faces.Add(new List<int>() { 15, 367, 518, 558, 386 });
			faces.Add(new List<int>() { 15, 367, 519, 559, 387 });
			faces.Add(new List<int>() { 15, 386, 290, 291, 387 });
			faces.Add(new List<int>() { 16, 368, 272, 273, 369 });
			faces.Add(new List<int>() { 16, 368, 520, 568, 392 });
			faces.Add(new List<int>() { 16, 368, 522, 570, 393 });
			faces.Add(new List<int>() { 16, 369, 521, 569, 392 });
			faces.Add(new List<int>() { 16, 369, 523, 571, 393 });
			faces.Add(new List<int>() { 16, 392, 296, 297, 393 });
			faces.Add(new List<int>() { 17, 370, 274, 275, 371 });
			faces.Add(new List<int>() { 17, 370, 524, 572, 394 });
			faces.Add(new List<int>() { 17, 370, 526, 574, 395 });
			faces.Add(new List<int>() { 17, 371, 525, 573, 394 });
			faces.Add(new List<int>() { 17, 371, 527, 575, 395 });
			faces.Add(new List<int>() { 17, 394, 298, 299, 395 });
			faces.Add(new List<int>() { 18, 372, 276, 278, 374 });
			faces.Add(new List<int>() { 18, 372, 528, 584, 400 });
			faces.Add(new List<int>() { 18, 372, 532, 588, 402 });
			faces.Add(new List<int>() { 18, 374, 530, 586, 400 });
			faces.Add(new List<int>() { 18, 374, 534, 590, 402 });
			faces.Add(new List<int>() { 18, 400, 304, 306, 402 });
			faces.Add(new List<int>() { 19, 373, 277, 279, 375 });
			faces.Add(new List<int>() { 19, 373, 529, 585, 401 });
			faces.Add(new List<int>() { 19, 373, 533, 589, 403 });
			faces.Add(new List<int>() { 19, 375, 531, 587, 401 });
			faces.Add(new List<int>() { 19, 375, 535, 591, 403 });
			faces.Add(new List<int>() { 19, 401, 305, 307, 403 });
			faces.Add(new List<int>() { 20, 376, 280, 282, 378 });
			faces.Add(new List<int>() { 20, 376, 536, 576, 396 });
			faces.Add(new List<int>() { 20, 376, 537, 577, 397 });
			faces.Add(new List<int>() { 20, 378, 540, 580, 396 });
			faces.Add(new List<int>() { 20, 378, 541, 581, 397 });
			faces.Add(new List<int>() { 20, 396, 300, 301, 397 });
			faces.Add(new List<int>() { 21, 377, 281, 283, 379 });
			faces.Add(new List<int>() { 21, 377, 538, 578, 398 });
			faces.Add(new List<int>() { 21, 377, 539, 579, 399 });
			faces.Add(new List<int>() { 21, 379, 542, 582, 398 });
			faces.Add(new List<int>() { 21, 379, 543, 583, 399 });
			faces.Add(new List<int>() { 21, 398, 302, 303, 399 });
			faces.Add(new List<int>() { 22, 380, 284, 285, 381 });
			faces.Add(new List<int>() { 22, 380, 544, 592, 404 });
			faces.Add(new List<int>() { 22, 380, 546, 594, 405 });
			faces.Add(new List<int>() { 22, 381, 545, 593, 404 });
			faces.Add(new List<int>() { 22, 381, 547, 595, 405 });
			faces.Add(new List<int>() { 22, 404, 308, 309, 405 });
			faces.Add(new List<int>() { 23, 382, 286, 287, 383 });
			faces.Add(new List<int>() { 23, 382, 548, 596, 406 });
			faces.Add(new List<int>() { 23, 382, 550, 598, 407 });
			faces.Add(new List<int>() { 23, 383, 549, 597, 406 });
			faces.Add(new List<int>() { 23, 383, 551, 599, 407 });
			faces.Add(new List<int>() { 23, 406, 310, 311, 407 });
			faces.Add(new List<int>() { 24, 152, 216, 312, 408 });
			faces.Add(new List<int>() { 24, 152, 244, 340, 464 });
			faces.Add(new List<int>() { 24, 152, 264, 360, 504 });
			faces.Add(new List<int>() { 24, 408, 88, 424, 464 });
			faces.Add(new List<int>() { 24, 408, 456, 104, 504 });
			faces.Add(new List<int>() { 24, 464, 112, 560, 504 });
			faces.Add(new List<int>() { 25, 153, 217, 313, 409 });
			faces.Add(new List<int>() { 25, 153, 245, 341, 465 });
			faces.Add(new List<int>() { 25, 153, 265, 361, 505 });
			faces.Add(new List<int>() { 25, 409, 89, 425, 465 });
			faces.Add(new List<int>() { 25, 409, 457, 105, 505 });
			faces.Add(new List<int>() { 25, 465, 113, 561, 505 });
			faces.Add(new List<int>() { 26, 154, 218, 314, 410 });
			faces.Add(new List<int>() { 26, 154, 244, 340, 466 });
			faces.Add(new List<int>() { 26, 154, 266, 362, 506 });
			faces.Add(new List<int>() { 26, 410, 90, 426, 466 });
			faces.Add(new List<int>() { 26, 410, 458, 106, 506 });
			faces.Add(new List<int>() { 26, 466, 114, 562, 506 });
			faces.Add(new List<int>() { 27, 155, 219, 315, 411 });
			faces.Add(new List<int>() { 27, 155, 245, 341, 467 });
			faces.Add(new List<int>() { 27, 155, 267, 363, 507 });
			faces.Add(new List<int>() { 27, 411, 91, 427, 467 });
			faces.Add(new List<int>() { 27, 411, 459, 107, 507 });
			faces.Add(new List<int>() { 27, 467, 115, 563, 507 });
			faces.Add(new List<int>() { 28, 156, 224, 320, 416 });
			faces.Add(new List<int>() { 28, 156, 240, 336, 456 });
			faces.Add(new List<int>() { 28, 156, 268, 364, 512 });
			faces.Add(new List<int>() { 28, 416, 88, 408, 456 });
			faces.Add(new List<int>() { 28, 416, 472, 116, 512 });
			faces.Add(new List<int>() { 28, 456, 104, 552, 512 });
			faces.Add(new List<int>() { 29, 157, 225, 321, 417 });
			faces.Add(new List<int>() { 29, 157, 241, 337, 457 });
			faces.Add(new List<int>() { 29, 157, 268, 364, 513 });
			faces.Add(new List<int>() { 29, 417, 89, 409, 457 });
			faces.Add(new List<int>() { 29, 417, 473, 117, 513 });
			faces.Add(new List<int>() { 29, 457, 105, 553, 513 });
			faces.Add(new List<int>() { 30, 158, 226, 322, 418 });
			faces.Add(new List<int>() { 30, 158, 242, 338, 458 });
			faces.Add(new List<int>() { 30, 158, 269, 365, 514 });
			faces.Add(new List<int>() { 30, 418, 90, 410, 458 });
			faces.Add(new List<int>() { 30, 418, 474, 118, 514 });
			faces.Add(new List<int>() { 30, 458, 106, 554, 514 });
			faces.Add(new List<int>() { 31, 159, 227, 323, 419 });
			faces.Add(new List<int>() { 31, 159, 243, 339, 459 });
			faces.Add(new List<int>() { 31, 159, 269, 365, 515 });
			faces.Add(new List<int>() { 31, 419, 91, 411, 459 });
			faces.Add(new List<int>() { 31, 419, 475, 119, 515 });
			faces.Add(new List<int>() { 31, 459, 107, 555, 515 });
			faces.Add(new List<int>() { 32, 160, 220, 316, 412 });
			faces.Add(new List<int>() { 32, 160, 246, 342, 468 });
			faces.Add(new List<int>() { 32, 160, 264, 360, 508 });
			faces.Add(new List<int>() { 32, 412, 92, 428, 468 });
			faces.Add(new List<int>() { 32, 412, 460, 108, 508 });
			faces.Add(new List<int>() { 32, 468, 120, 564, 508 });
			faces.Add(new List<int>() { 33, 161, 221, 317, 413 });
			faces.Add(new List<int>() { 33, 161, 247, 343, 469 });
			faces.Add(new List<int>() { 33, 161, 265, 361, 509 });
			faces.Add(new List<int>() { 33, 413, 93, 429, 469 });
			faces.Add(new List<int>() { 33, 413, 461, 109, 509 });
			faces.Add(new List<int>() { 33, 469, 121, 565, 509 });
			faces.Add(new List<int>() { 34, 162, 222, 318, 414 });
			faces.Add(new List<int>() { 34, 162, 246, 342, 470 });
			faces.Add(new List<int>() { 34, 162, 266, 362, 510 });
			faces.Add(new List<int>() { 34, 414, 94, 430, 470 });
			faces.Add(new List<int>() { 34, 414, 462, 110, 510 });
			faces.Add(new List<int>() { 34, 470, 122, 566, 510 });
			faces.Add(new List<int>() { 35, 163, 223, 319, 415 });
			faces.Add(new List<int>() { 35, 163, 247, 343, 471 });
			faces.Add(new List<int>() { 35, 163, 267, 363, 511 });
			faces.Add(new List<int>() { 35, 415, 95, 431, 471 });
			faces.Add(new List<int>() { 35, 415, 463, 111, 511 });
			faces.Add(new List<int>() { 35, 471, 123, 567, 511 });
			faces.Add(new List<int>() { 36, 164, 228, 324, 420 });
			faces.Add(new List<int>() { 36, 164, 240, 336, 460 });
			faces.Add(new List<int>() { 36, 164, 270, 366, 516 });
			faces.Add(new List<int>() { 36, 420, 92, 412, 460 });
			faces.Add(new List<int>() { 36, 420, 476, 124, 516 });
			faces.Add(new List<int>() { 36, 460, 108, 556, 516 });
			faces.Add(new List<int>() { 37, 165, 229, 325, 421 });
			faces.Add(new List<int>() { 37, 165, 241, 337, 461 });
			faces.Add(new List<int>() { 37, 165, 270, 366, 517 });
			faces.Add(new List<int>() { 37, 421, 93, 413, 461 });
			faces.Add(new List<int>() { 37, 421, 477, 125, 517 });
			faces.Add(new List<int>() { 37, 461, 109, 557, 517 });
			faces.Add(new List<int>() { 38, 166, 230, 326, 422 });
			faces.Add(new List<int>() { 38, 166, 242, 338, 462 });
			faces.Add(new List<int>() { 38, 166, 271, 367, 518 });
			faces.Add(new List<int>() { 38, 422, 94, 414, 462 });
			faces.Add(new List<int>() { 38, 422, 478, 126, 518 });
			faces.Add(new List<int>() { 38, 462, 110, 558, 518 });
			faces.Add(new List<int>() { 39, 167, 231, 327, 423 });
			faces.Add(new List<int>() { 39, 167, 243, 339, 463 });
			faces.Add(new List<int>() { 39, 167, 271, 367, 519 });
			faces.Add(new List<int>() { 39, 423, 95, 415, 463 });
			faces.Add(new List<int>() { 39, 423, 479, 127, 519 });
			faces.Add(new List<int>() { 39, 463, 111, 559, 519 });
			faces.Add(new List<int>() { 40, 168, 232, 328, 424 });
			faces.Add(new List<int>() { 40, 168, 248, 344, 472 });
			faces.Add(new List<int>() { 40, 168, 272, 368, 520 });
			faces.Add(new List<int>() { 40, 424, 88, 416, 472 });
			faces.Add(new List<int>() { 40, 424, 464, 112, 520 });
			faces.Add(new List<int>() { 40, 472, 116, 568, 520 });
			faces.Add(new List<int>() { 41, 169, 233, 329, 425 });
			faces.Add(new List<int>() { 41, 169, 248, 344, 473 });
			faces.Add(new List<int>() { 41, 169, 273, 369, 521 });
			faces.Add(new List<int>() { 41, 425, 89, 417, 473 });
			faces.Add(new List<int>() { 41, 425, 465, 113, 521 });
			faces.Add(new List<int>() { 41, 473, 117, 569, 521 });
			faces.Add(new List<int>() { 42, 170, 234, 330, 426 });
			faces.Add(new List<int>() { 42, 170, 249, 345, 474 });
			faces.Add(new List<int>() { 42, 170, 272, 368, 522 });
			faces.Add(new List<int>() { 42, 426, 90, 418, 474 });
			faces.Add(new List<int>() { 42, 426, 466, 114, 522 });
			faces.Add(new List<int>() { 42, 474, 118, 570, 522 });
			faces.Add(new List<int>() { 43, 171, 235, 331, 427 });
			faces.Add(new List<int>() { 43, 171, 249, 345, 475 });
			faces.Add(new List<int>() { 43, 171, 273, 369, 523 });
			faces.Add(new List<int>() { 43, 427, 91, 419, 475 });
			faces.Add(new List<int>() { 43, 427, 467, 115, 523 });
			faces.Add(new List<int>() { 43, 475, 119, 571, 523 });
			faces.Add(new List<int>() { 44, 172, 236, 332, 428 });
			faces.Add(new List<int>() { 44, 172, 250, 346, 476 });
			faces.Add(new List<int>() { 44, 172, 274, 370, 524 });
			faces.Add(new List<int>() { 44, 428, 92, 420, 476 });
			faces.Add(new List<int>() { 44, 428, 468, 120, 524 });
			faces.Add(new List<int>() { 44, 476, 124, 572, 524 });
			faces.Add(new List<int>() { 45, 173, 237, 333, 429 });
			faces.Add(new List<int>() { 45, 173, 250, 346, 477 });
			faces.Add(new List<int>() { 45, 173, 275, 371, 525 });
			faces.Add(new List<int>() { 45, 429, 93, 421, 477 });
			faces.Add(new List<int>() { 45, 429, 469, 121, 525 });
			faces.Add(new List<int>() { 45, 477, 125, 573, 525 });
			faces.Add(new List<int>() { 46, 174, 238, 334, 430 });
			faces.Add(new List<int>() { 46, 174, 251, 347, 478 });
			faces.Add(new List<int>() { 46, 174, 274, 370, 526 });
			faces.Add(new List<int>() { 46, 430, 94, 422, 478 });
			faces.Add(new List<int>() { 46, 430, 470, 122, 526 });
			faces.Add(new List<int>() { 46, 478, 126, 574, 526 });
			faces.Add(new List<int>() { 47, 175, 239, 335, 431 });
			faces.Add(new List<int>() { 47, 175, 251, 347, 479 });
			faces.Add(new List<int>() { 47, 175, 275, 371, 527 });
			faces.Add(new List<int>() { 47, 431, 95, 423, 479 });
			faces.Add(new List<int>() { 47, 431, 471, 123, 527 });
			faces.Add(new List<int>() { 47, 479, 127, 575, 527 });
			faces.Add(new List<int>() { 48, 176, 216, 312, 432 });
			faces.Add(new List<int>() { 48, 176, 256, 352, 488 });
			faces.Add(new List<int>() { 48, 176, 276, 372, 528 });
			faces.Add(new List<int>() { 48, 432, 96, 448, 488 });
			faces.Add(new List<int>() { 48, 432, 480, 128, 528 });
			faces.Add(new List<int>() { 48, 488, 136, 584, 528 });
			faces.Add(new List<int>() { 49, 177, 217, 313, 433 });
			faces.Add(new List<int>() { 49, 177, 257, 353, 489 });
			faces.Add(new List<int>() { 49, 177, 277, 373, 529 });
			faces.Add(new List<int>() { 49, 433, 97, 449, 489 });
			faces.Add(new List<int>() { 49, 433, 481, 129, 529 });
			faces.Add(new List<int>() { 49, 489, 137, 585, 529 });
			faces.Add(new List<int>() { 50, 178, 218, 314, 434 });
			faces.Add(new List<int>() { 50, 178, 256, 352, 490 });
			faces.Add(new List<int>() { 50, 178, 278, 374, 530 });
			faces.Add(new List<int>() { 50, 434, 98, 450, 490 });
			faces.Add(new List<int>() { 50, 434, 482, 130, 530 });
			faces.Add(new List<int>() { 50, 490, 138, 586, 530 });
			faces.Add(new List<int>() { 51, 179, 219, 315, 435 });
			faces.Add(new List<int>() { 51, 179, 257, 353, 491 });
			faces.Add(new List<int>() { 51, 179, 279, 375, 531 });
			faces.Add(new List<int>() { 51, 435, 99, 451, 491 });
			faces.Add(new List<int>() { 51, 435, 483, 131, 531 });
			faces.Add(new List<int>() { 51, 491, 139, 587, 531 });
			faces.Add(new List<int>() { 52, 180, 224, 320, 440 });
			faces.Add(new List<int>() { 52, 180, 252, 348, 480 });
			faces.Add(new List<int>() { 52, 180, 280, 376, 536 });
			faces.Add(new List<int>() { 52, 440, 96, 432, 480 });
			faces.Add(new List<int>() { 52, 440, 496, 140, 536 });
			faces.Add(new List<int>() { 52, 480, 128, 576, 536 });
			faces.Add(new List<int>() { 53, 181, 225, 321, 441 });
			faces.Add(new List<int>() { 53, 181, 253, 349, 481 });
			faces.Add(new List<int>() { 53, 181, 280, 376, 537 });
			faces.Add(new List<int>() { 53, 441, 97, 433, 481 });
			faces.Add(new List<int>() { 53, 441, 497, 141, 537 });
			faces.Add(new List<int>() { 53, 481, 129, 577, 537 });
			faces.Add(new List<int>() { 54, 182, 226, 322, 442 });
			faces.Add(new List<int>() { 54, 182, 254, 350, 482 });
			faces.Add(new List<int>() { 54, 182, 281, 377, 538 });
			faces.Add(new List<int>() { 54, 442, 98, 434, 482 });
			faces.Add(new List<int>() { 54, 442, 498, 142, 538 });
			faces.Add(new List<int>() { 54, 482, 130, 578, 538 });
			faces.Add(new List<int>() { 55, 183, 227, 323, 443 });
			faces.Add(new List<int>() { 55, 183, 255, 351, 483 });
			faces.Add(new List<int>() { 55, 183, 281, 377, 539 });
			faces.Add(new List<int>() { 55, 443, 99, 435, 483 });
			faces.Add(new List<int>() { 55, 443, 499, 143, 539 });
			faces.Add(new List<int>() { 55, 483, 131, 579, 539 });
			faces.Add(new List<int>() { 56, 184, 220, 316, 436 });
			faces.Add(new List<int>() { 56, 184, 258, 354, 492 });
			faces.Add(new List<int>() { 56, 184, 276, 372, 532 });
			faces.Add(new List<int>() { 56, 436, 100, 452, 492 });
			faces.Add(new List<int>() { 56, 436, 484, 132, 532 });
			faces.Add(new List<int>() { 56, 492, 144, 588, 532 });
			faces.Add(new List<int>() { 57, 185, 221, 317, 437 });
			faces.Add(new List<int>() { 57, 185, 259, 355, 493 });
			faces.Add(new List<int>() { 57, 185, 277, 373, 533 });
			faces.Add(new List<int>() { 57, 437, 101, 453, 493 });
			faces.Add(new List<int>() { 57, 437, 485, 133, 533 });
			faces.Add(new List<int>() { 57, 493, 145, 589, 533 });
			faces.Add(new List<int>() { 58, 186, 222, 318, 438 });
			faces.Add(new List<int>() { 58, 186, 258, 354, 494 });
			faces.Add(new List<int>() { 58, 186, 278, 374, 534 });
			faces.Add(new List<int>() { 58, 438, 102, 454, 494 });
			faces.Add(new List<int>() { 58, 438, 486, 134, 534 });
			faces.Add(new List<int>() { 58, 494, 146, 590, 534 });
			faces.Add(new List<int>() { 59, 187, 223, 319, 439 });
			faces.Add(new List<int>() { 59, 187, 259, 355, 495 });
			faces.Add(new List<int>() { 59, 187, 279, 375, 535 });
			faces.Add(new List<int>() { 59, 439, 103, 455, 495 });
			faces.Add(new List<int>() { 59, 439, 487, 135, 535 });
			faces.Add(new List<int>() { 59, 495, 147, 591, 535 });
			faces.Add(new List<int>() { 60, 188, 228, 324, 444 });
			faces.Add(new List<int>() { 60, 188, 252, 348, 484 });
			faces.Add(new List<int>() { 60, 188, 282, 378, 540 });
			faces.Add(new List<int>() { 60, 444, 100, 436, 484 });
			faces.Add(new List<int>() { 60, 444, 500, 148, 540 });
			faces.Add(new List<int>() { 60, 484, 132, 580, 540 });
			faces.Add(new List<int>() { 61, 189, 229, 325, 445 });
			faces.Add(new List<int>() { 61, 189, 253, 349, 485 });
			faces.Add(new List<int>() { 61, 189, 282, 378, 541 });
			faces.Add(new List<int>() { 61, 445, 101, 437, 485 });
			faces.Add(new List<int>() { 61, 445, 501, 149, 541 });
			faces.Add(new List<int>() { 61, 485, 133, 581, 541 });
			faces.Add(new List<int>() { 62, 190, 230, 326, 446 });
			faces.Add(new List<int>() { 62, 190, 254, 350, 486 });
			faces.Add(new List<int>() { 62, 190, 283, 379, 542 });
			faces.Add(new List<int>() { 62, 446, 102, 438, 486 });
			faces.Add(new List<int>() { 62, 446, 502, 150, 542 });
			faces.Add(new List<int>() { 62, 486, 134, 582, 542 });
			faces.Add(new List<int>() { 63, 191, 231, 327, 447 });
			faces.Add(new List<int>() { 63, 191, 255, 351, 487 });
			faces.Add(new List<int>() { 63, 191, 283, 379, 543 });
			faces.Add(new List<int>() { 63, 447, 103, 439, 487 });
			faces.Add(new List<int>() { 63, 447, 503, 151, 543 });
			faces.Add(new List<int>() { 63, 487, 135, 583, 543 });
			faces.Add(new List<int>() { 64, 192, 232, 328, 448 });
			faces.Add(new List<int>() { 64, 192, 260, 356, 496 });
			faces.Add(new List<int>() { 64, 192, 284, 380, 544 });
			faces.Add(new List<int>() { 64, 448, 96, 440, 496 });
			faces.Add(new List<int>() { 64, 448, 488, 136, 544 });
			faces.Add(new List<int>() { 64, 496, 140, 592, 544 });
			faces.Add(new List<int>() { 65, 193, 233, 329, 449 });
			faces.Add(new List<int>() { 65, 193, 260, 356, 497 });
			faces.Add(new List<int>() { 65, 193, 285, 381, 545 });
			faces.Add(new List<int>() { 65, 449, 97, 441, 497 });
			faces.Add(new List<int>() { 65, 449, 489, 137, 545 });
			faces.Add(new List<int>() { 65, 497, 141, 593, 545 });
			faces.Add(new List<int>() { 66, 194, 234, 330, 450 });
			faces.Add(new List<int>() { 66, 194, 261, 357, 498 });
			faces.Add(new List<int>() { 66, 194, 284, 380, 546 });
			faces.Add(new List<int>() { 66, 450, 98, 442, 498 });
			faces.Add(new List<int>() { 66, 450, 490, 138, 546 });
			faces.Add(new List<int>() { 66, 498, 142, 594, 546 });
			faces.Add(new List<int>() { 67, 195, 235, 331, 451 });
			faces.Add(new List<int>() { 67, 195, 261, 357, 499 });
			faces.Add(new List<int>() { 67, 195, 285, 381, 547 });
			faces.Add(new List<int>() { 67, 451, 99, 443, 499 });
			faces.Add(new List<int>() { 67, 451, 491, 139, 547 });
			faces.Add(new List<int>() { 67, 499, 143, 595, 547 });
			faces.Add(new List<int>() { 68, 196, 236, 332, 452 });
			faces.Add(new List<int>() { 68, 196, 262, 358, 500 });
			faces.Add(new List<int>() { 68, 196, 286, 382, 548 });
			faces.Add(new List<int>() { 68, 452, 100, 444, 500 });
			faces.Add(new List<int>() { 68, 452, 492, 144, 548 });
			faces.Add(new List<int>() { 68, 500, 148, 596, 548 });
			faces.Add(new List<int>() { 69, 197, 237, 333, 453 });
			faces.Add(new List<int>() { 69, 197, 262, 358, 501 });
			faces.Add(new List<int>() { 69, 197, 287, 383, 549 });
			faces.Add(new List<int>() { 69, 453, 101, 445, 501 });
			faces.Add(new List<int>() { 69, 453, 493, 145, 549 });
			faces.Add(new List<int>() { 69, 501, 149, 597, 549 });
			faces.Add(new List<int>() { 70, 198, 238, 334, 454 });
			faces.Add(new List<int>() { 70, 198, 263, 359, 502 });
			faces.Add(new List<int>() { 70, 198, 286, 382, 550 });
			faces.Add(new List<int>() { 70, 454, 102, 446, 502 });
			faces.Add(new List<int>() { 70, 454, 494, 146, 550 });
			faces.Add(new List<int>() { 70, 502, 150, 598, 550 });
			faces.Add(new List<int>() { 71, 199, 239, 335, 455 });
			faces.Add(new List<int>() { 71, 199, 263, 359, 503 });
			faces.Add(new List<int>() { 71, 199, 287, 383, 551 });
			faces.Add(new List<int>() { 71, 455, 103, 447, 503 });
			faces.Add(new List<int>() { 71, 455, 495, 147, 551 });
			faces.Add(new List<int>() { 71, 503, 151, 599, 551 });
			faces.Add(new List<int>() { 72, 200, 288, 384, 552 });
			faces.Add(new List<int>() { 72, 200, 292, 388, 560 });
			faces.Add(new List<int>() { 72, 200, 296, 392, 568 });
			faces.Add(new List<int>() { 72, 552, 104, 504, 560 });
			faces.Add(new List<int>() { 72, 552, 512, 116, 568 });
			faces.Add(new List<int>() { 72, 560, 112, 520, 568 });
			faces.Add(new List<int>() { 73, 201, 289, 385, 553 });
			faces.Add(new List<int>() { 73, 201, 293, 389, 561 });
			faces.Add(new List<int>() { 73, 201, 296, 392, 569 });
			faces.Add(new List<int>() { 73, 553, 105, 505, 561 });
			faces.Add(new List<int>() { 73, 553, 513, 117, 569 });
			faces.Add(new List<int>() { 73, 561, 113, 521, 569 });
			faces.Add(new List<int>() { 74, 202, 290, 386, 554 });
			faces.Add(new List<int>() { 74, 202, 292, 388, 562 });
			faces.Add(new List<int>() { 74, 202, 297, 393, 570 });
			faces.Add(new List<int>() { 74, 554, 106, 506, 562 });
			faces.Add(new List<int>() { 74, 554, 514, 118, 570 });
			faces.Add(new List<int>() { 74, 562, 114, 522, 570 });
			faces.Add(new List<int>() { 75, 203, 291, 387, 555 });
			faces.Add(new List<int>() { 75, 203, 293, 389, 563 });
			faces.Add(new List<int>() { 75, 203, 297, 393, 571 });
			faces.Add(new List<int>() { 75, 555, 107, 507, 563 });
			faces.Add(new List<int>() { 75, 555, 515, 119, 571 });
			faces.Add(new List<int>() { 75, 563, 115, 523, 571 });
			faces.Add(new List<int>() { 76, 204, 288, 384, 556 });
			faces.Add(new List<int>() { 76, 204, 294, 390, 564 });
			faces.Add(new List<int>() { 76, 204, 298, 394, 572 });
			faces.Add(new List<int>() { 76, 556, 108, 508, 564 });
			faces.Add(new List<int>() { 76, 556, 516, 124, 572 });
			faces.Add(new List<int>() { 76, 564, 120, 524, 572 });
			faces.Add(new List<int>() { 77, 205, 289, 385, 557 });
			faces.Add(new List<int>() { 77, 205, 295, 391, 565 });
			faces.Add(new List<int>() { 77, 205, 298, 394, 573 });
			faces.Add(new List<int>() { 77, 557, 109, 509, 565 });
			faces.Add(new List<int>() { 77, 557, 517, 125, 573 });
			faces.Add(new List<int>() { 77, 565, 121, 525, 573 });
			faces.Add(new List<int>() { 78, 206, 290, 386, 558 });
			faces.Add(new List<int>() { 78, 206, 294, 390, 566 });
			faces.Add(new List<int>() { 78, 206, 299, 395, 574 });
			faces.Add(new List<int>() { 78, 558, 110, 510, 566 });
			faces.Add(new List<int>() { 78, 558, 518, 126, 574 });
			faces.Add(new List<int>() { 78, 566, 122, 526, 574 });
			faces.Add(new List<int>() { 79, 207, 291, 387, 559 });
			faces.Add(new List<int>() { 79, 207, 295, 391, 567 });
			faces.Add(new List<int>() { 79, 207, 299, 395, 575 });
			faces.Add(new List<int>() { 79, 559, 111, 511, 567 });
			faces.Add(new List<int>() { 79, 559, 519, 127, 575 });
			faces.Add(new List<int>() { 79, 567, 123, 527, 575 });
			faces.Add(new List<int>() { 80, 208, 300, 396, 576 });
			faces.Add(new List<int>() { 80, 208, 304, 400, 584 });
			faces.Add(new List<int>() { 80, 208, 308, 404, 592 });
			faces.Add(new List<int>() { 80, 576, 128, 528, 584 });
			faces.Add(new List<int>() { 80, 576, 536, 140, 592 });
			faces.Add(new List<int>() { 80, 584, 136, 544, 592 });
			faces.Add(new List<int>() { 81, 209, 301, 397, 577 });
			faces.Add(new List<int>() { 81, 209, 305, 401, 585 });
			faces.Add(new List<int>() { 81, 209, 308, 404, 593 });
			faces.Add(new List<int>() { 81, 577, 129, 529, 585 });
			faces.Add(new List<int>() { 81, 577, 537, 141, 593 });
			faces.Add(new List<int>() { 81, 585, 137, 545, 593 });
			faces.Add(new List<int>() { 82, 210, 302, 398, 578 });
			faces.Add(new List<int>() { 82, 210, 304, 400, 586 });
			faces.Add(new List<int>() { 82, 210, 309, 405, 594 });
			faces.Add(new List<int>() { 82, 578, 130, 530, 586 });
			faces.Add(new List<int>() { 82, 578, 538, 142, 594 });
			faces.Add(new List<int>() { 82, 586, 138, 546, 594 });
			faces.Add(new List<int>() { 83, 211, 303, 399, 579 });
			faces.Add(new List<int>() { 83, 211, 305, 401, 587 });
			faces.Add(new List<int>() { 83, 211, 309, 405, 595 });
			faces.Add(new List<int>() { 83, 579, 131, 531, 587 });
			faces.Add(new List<int>() { 83, 579, 539, 143, 595 });
			faces.Add(new List<int>() { 83, 587, 139, 547, 595 });
			faces.Add(new List<int>() { 84, 212, 300, 396, 580 });
			faces.Add(new List<int>() { 84, 212, 306, 402, 588 });
			faces.Add(new List<int>() { 84, 212, 310, 406, 596 });
			faces.Add(new List<int>() { 84, 580, 132, 532, 588 });
			faces.Add(new List<int>() { 84, 580, 540, 148, 596 });
			faces.Add(new List<int>() { 84, 588, 144, 548, 596 });
			faces.Add(new List<int>() { 85, 213, 301, 397, 581 });
			faces.Add(new List<int>() { 85, 213, 307, 403, 589 });
			faces.Add(new List<int>() { 85, 213, 310, 406, 597 });
			faces.Add(new List<int>() { 85, 581, 133, 533, 589 });
			faces.Add(new List<int>() { 85, 581, 541, 149, 597 });
			faces.Add(new List<int>() { 85, 589, 145, 549, 597 });
			faces.Add(new List<int>() { 86, 214, 302, 398, 582 });
			faces.Add(new List<int>() { 86, 214, 306, 402, 590 });
			faces.Add(new List<int>() { 86, 214, 311, 407, 598 });
			faces.Add(new List<int>() { 86, 582, 134, 534, 590 });
			faces.Add(new List<int>() { 86, 582, 542, 150, 598 });
			faces.Add(new List<int>() { 86, 590, 146, 550, 598 });
			faces.Add(new List<int>() { 87, 215, 303, 399, 583 });
			faces.Add(new List<int>() { 87, 215, 307, 403, 591 });
			faces.Add(new List<int>() { 87, 215, 311, 407, 599 });
			faces.Add(new List<int>() { 87, 583, 135, 535, 591 });
			faces.Add(new List<int>() { 87, 583, 543, 151, 599 });
			faces.Add(new List<int>() { 87, 591, 147, 551, 599 });
			faces.Add(new List<int>() { 88, 96, 432, 312, 408 });
			faces.Add(new List<int>() { 88, 96, 440, 320, 416 });
			faces.Add(new List<int>() { 88, 96, 448, 328, 424 });
			faces.Add(new List<int>() { 89, 97, 433, 313, 409 });
			faces.Add(new List<int>() { 89, 97, 441, 321, 417 });
			faces.Add(new List<int>() { 89, 97, 449, 329, 425 });
			faces.Add(new List<int>() { 90, 98, 434, 314, 410 });
			faces.Add(new List<int>() { 90, 98, 442, 322, 418 });
			faces.Add(new List<int>() { 90, 98, 450, 330, 426 });
			faces.Add(new List<int>() { 91, 99, 435, 315, 411 });
			faces.Add(new List<int>() { 91, 99, 443, 323, 419 });
			faces.Add(new List<int>() { 91, 99, 451, 331, 427 });
			faces.Add(new List<int>() { 92, 100, 436, 316, 412 });
			faces.Add(new List<int>() { 92, 100, 444, 324, 420 });
			faces.Add(new List<int>() { 92, 100, 452, 332, 428 });
			faces.Add(new List<int>() { 93, 101, 437, 317, 413 });
			faces.Add(new List<int>() { 93, 101, 445, 325, 421 });
			faces.Add(new List<int>() { 93, 101, 453, 333, 429 });
			faces.Add(new List<int>() { 94, 102, 438, 318, 414 });
			faces.Add(new List<int>() { 94, 102, 446, 326, 422 });
			faces.Add(new List<int>() { 94, 102, 454, 334, 430 });
			faces.Add(new List<int>() { 95, 103, 439, 319, 415 });
			faces.Add(new List<int>() { 95, 103, 447, 327, 423 });
			faces.Add(new List<int>() { 95, 103, 455, 335, 431 });
			faces.Add(new List<int>() { 104, 108, 460, 336, 456 });
			faces.Add(new List<int>() { 104, 108, 508, 360, 504 });
			faces.Add(new List<int>() { 104, 108, 556, 384, 552 });
			faces.Add(new List<int>() { 105, 109, 461, 337, 457 });
			faces.Add(new List<int>() { 105, 109, 509, 361, 505 });
			faces.Add(new List<int>() { 105, 109, 557, 385, 553 });
			faces.Add(new List<int>() { 106, 110, 462, 338, 458 });
			faces.Add(new List<int>() { 106, 110, 510, 362, 506 });
			faces.Add(new List<int>() { 106, 110, 558, 386, 554 });
			faces.Add(new List<int>() { 107, 111, 463, 339, 459 });
			faces.Add(new List<int>() { 107, 111, 511, 363, 507 });
			faces.Add(new List<int>() { 107, 111, 559, 387, 555 });
			faces.Add(new List<int>() { 112, 114, 466, 340, 464 });
			faces.Add(new List<int>() { 112, 114, 522, 368, 520 });
			faces.Add(new List<int>() { 112, 114, 562, 388, 560 });
			faces.Add(new List<int>() { 113, 115, 467, 341, 465 });
			faces.Add(new List<int>() { 113, 115, 523, 369, 521 });
			faces.Add(new List<int>() { 113, 115, 563, 389, 561 });
			faces.Add(new List<int>() { 116, 117, 473, 344, 472 });
			faces.Add(new List<int>() { 116, 117, 513, 364, 512 });
			faces.Add(new List<int>() { 116, 117, 569, 392, 568 });
			faces.Add(new List<int>() { 118, 119, 475, 345, 474 });
			faces.Add(new List<int>() { 118, 119, 515, 365, 514 });
			faces.Add(new List<int>() { 118, 119, 571, 393, 570 });
			faces.Add(new List<int>() { 120, 122, 470, 342, 468 });
			faces.Add(new List<int>() { 120, 122, 526, 370, 524 });
			faces.Add(new List<int>() { 120, 122, 566, 390, 564 });
			faces.Add(new List<int>() { 121, 123, 471, 343, 469 });
			faces.Add(new List<int>() { 121, 123, 527, 371, 525 });
			faces.Add(new List<int>() { 121, 123, 567, 391, 565 });
			faces.Add(new List<int>() { 124, 125, 477, 346, 476 });
			faces.Add(new List<int>() { 124, 125, 517, 366, 516 });
			faces.Add(new List<int>() { 124, 125, 573, 394, 572 });
			faces.Add(new List<int>() { 126, 127, 479, 347, 478 });
			faces.Add(new List<int>() { 126, 127, 519, 367, 518 });
			faces.Add(new List<int>() { 126, 127, 575, 395, 574 });
			faces.Add(new List<int>() { 128, 132, 484, 348, 480 });
			faces.Add(new List<int>() { 128, 132, 532, 372, 528 });
			faces.Add(new List<int>() { 128, 132, 580, 396, 576 });
			faces.Add(new List<int>() { 129, 133, 485, 349, 481 });
			faces.Add(new List<int>() { 129, 133, 533, 373, 529 });
			faces.Add(new List<int>() { 129, 133, 581, 397, 577 });
			faces.Add(new List<int>() { 130, 134, 486, 350, 482 });
			faces.Add(new List<int>() { 130, 134, 534, 374, 530 });
			faces.Add(new List<int>() { 130, 134, 582, 398, 578 });
			faces.Add(new List<int>() { 131, 135, 487, 351, 483 });
			faces.Add(new List<int>() { 131, 135, 535, 375, 531 });
			faces.Add(new List<int>() { 131, 135, 583, 399, 579 });
			faces.Add(new List<int>() { 136, 138, 490, 352, 488 });
			faces.Add(new List<int>() { 136, 138, 546, 380, 544 });
			faces.Add(new List<int>() { 136, 138, 586, 400, 584 });
			faces.Add(new List<int>() { 137, 139, 491, 353, 489 });
			faces.Add(new List<int>() { 137, 139, 547, 381, 545 });
			faces.Add(new List<int>() { 137, 139, 587, 401, 585 });
			faces.Add(new List<int>() { 140, 141, 497, 356, 496 });
			faces.Add(new List<int>() { 140, 141, 537, 376, 536 });
			faces.Add(new List<int>() { 140, 141, 593, 404, 592 });
			faces.Add(new List<int>() { 142, 143, 499, 357, 498 });
			faces.Add(new List<int>() { 142, 143, 539, 377, 538 });
			faces.Add(new List<int>() { 142, 143, 595, 405, 594 });
			faces.Add(new List<int>() { 144, 146, 494, 354, 492 });
			faces.Add(new List<int>() { 144, 146, 550, 382, 548 });
			faces.Add(new List<int>() { 144, 146, 590, 402, 588 });
			faces.Add(new List<int>() { 145, 147, 495, 355, 493 });
			faces.Add(new List<int>() { 145, 147, 551, 383, 549 });
			faces.Add(new List<int>() { 145, 147, 591, 403, 589 });
			faces.Add(new List<int>() { 148, 149, 501, 358, 500 });
			faces.Add(new List<int>() { 148, 149, 541, 378, 540 });
			faces.Add(new List<int>() { 148, 149, 597, 406, 596 });
			faces.Add(new List<int>() { 150, 151, 503, 359, 502 });
			faces.Add(new List<int>() { 150, 151, 543, 379, 542 });
			faces.Add(new List<int>() { 150, 151, 599, 407, 598 });
			faces.Add(new List<int>() { 152, 216, 176, 256, 244 });
			faces.Add(new List<int>() { 152, 216, 220, 160, 264 });
			faces.Add(new List<int>() { 152, 244, 154, 266, 264 });
			faces.Add(new List<int>() { 153, 217, 177, 257, 245 });
			faces.Add(new List<int>() { 153, 217, 221, 161, 265 });
			faces.Add(new List<int>() { 153, 245, 155, 267, 265 });
			faces.Add(new List<int>() { 154, 218, 178, 256, 244 });
			faces.Add(new List<int>() { 154, 218, 222, 162, 266 });
			faces.Add(new List<int>() { 155, 219, 179, 257, 245 });
			faces.Add(new List<int>() { 155, 219, 223, 163, 267 });
			faces.Add(new List<int>() { 156, 224, 180, 252, 240 });
			faces.Add(new List<int>() { 156, 224, 225, 157, 268 });
			faces.Add(new List<int>() { 156, 240, 164, 270, 268 });
			faces.Add(new List<int>() { 157, 225, 181, 253, 241 });
			faces.Add(new List<int>() { 157, 241, 165, 270, 268 });
			faces.Add(new List<int>() { 158, 226, 182, 254, 242 });
			faces.Add(new List<int>() { 158, 226, 227, 159, 269 });
			faces.Add(new List<int>() { 158, 242, 166, 271, 269 });
			faces.Add(new List<int>() { 159, 227, 183, 255, 243 });
			faces.Add(new List<int>() { 159, 243, 167, 271, 269 });
			faces.Add(new List<int>() { 160, 220, 184, 258, 246 });
			faces.Add(new List<int>() { 160, 246, 162, 266, 264 });
			faces.Add(new List<int>() { 161, 221, 185, 259, 247 });
			faces.Add(new List<int>() { 161, 247, 163, 267, 265 });
			faces.Add(new List<int>() { 162, 222, 186, 258, 246 });
			faces.Add(new List<int>() { 163, 223, 187, 259, 247 });
			faces.Add(new List<int>() { 164, 228, 188, 252, 240 });
			faces.Add(new List<int>() { 164, 228, 229, 165, 270 });
			faces.Add(new List<int>() { 165, 229, 189, 253, 241 });
			faces.Add(new List<int>() { 166, 230, 190, 254, 242 });
			faces.Add(new List<int>() { 166, 230, 231, 167, 271 });
			faces.Add(new List<int>() { 167, 231, 191, 255, 243 });
			faces.Add(new List<int>() { 168, 232, 192, 260, 248 });
			faces.Add(new List<int>() { 168, 232, 234, 170, 272 });
			faces.Add(new List<int>() { 168, 248, 169, 273, 272 });
			faces.Add(new List<int>() { 169, 233, 193, 260, 248 });
			faces.Add(new List<int>() { 169, 233, 235, 171, 273 });
			faces.Add(new List<int>() { 170, 234, 194, 261, 249 });
			faces.Add(new List<int>() { 170, 249, 171, 273, 272 });
			faces.Add(new List<int>() { 171, 235, 195, 261, 249 });
			faces.Add(new List<int>() { 172, 236, 196, 262, 250 });
			faces.Add(new List<int>() { 172, 236, 238, 174, 274 });
			faces.Add(new List<int>() { 172, 250, 173, 275, 274 });
			faces.Add(new List<int>() { 173, 237, 197, 262, 250 });
			faces.Add(new List<int>() { 173, 237, 239, 175, 275 });
			faces.Add(new List<int>() { 174, 238, 198, 263, 251 });
			faces.Add(new List<int>() { 174, 251, 175, 275, 274 });
			faces.Add(new List<int>() { 175, 239, 199, 263, 251 });
			faces.Add(new List<int>() { 176, 216, 220, 184, 276 });
			faces.Add(new List<int>() { 176, 256, 178, 278, 276 });
			faces.Add(new List<int>() { 177, 217, 221, 185, 277 });
			faces.Add(new List<int>() { 177, 257, 179, 279, 277 });
			faces.Add(new List<int>() { 178, 218, 222, 186, 278 });
			faces.Add(new List<int>() { 179, 219, 223, 187, 279 });
			faces.Add(new List<int>() { 180, 224, 225, 181, 280 });
			faces.Add(new List<int>() { 180, 252, 188, 282, 280 });
			faces.Add(new List<int>() { 181, 253, 189, 282, 280 });
			faces.Add(new List<int>() { 182, 226, 227, 183, 281 });
			faces.Add(new List<int>() { 182, 254, 190, 283, 281 });
			faces.Add(new List<int>() { 183, 255, 191, 283, 281 });
			faces.Add(new List<int>() { 184, 258, 186, 278, 276 });
			faces.Add(new List<int>() { 185, 259, 187, 279, 277 });
			faces.Add(new List<int>() { 188, 228, 229, 189, 282 });
			faces.Add(new List<int>() { 190, 230, 231, 191, 283 });
			faces.Add(new List<int>() { 192, 232, 234, 194, 284 });
			faces.Add(new List<int>() { 192, 260, 193, 285, 284 });
			faces.Add(new List<int>() { 193, 233, 235, 195, 285 });
			faces.Add(new List<int>() { 194, 261, 195, 285, 284 });
			faces.Add(new List<int>() { 196, 236, 238, 198, 286 });
			faces.Add(new List<int>() { 196, 262, 197, 287, 286 });
			faces.Add(new List<int>() { 197, 237, 239, 199, 287 });
			faces.Add(new List<int>() { 198, 263, 199, 287, 286 });
			faces.Add(new List<int>() { 200, 288, 204, 294, 292 });
			faces.Add(new List<int>() { 200, 288, 289, 201, 296 });
			faces.Add(new List<int>() { 200, 292, 202, 297, 296 });
			faces.Add(new List<int>() { 201, 289, 205, 295, 293 });
			faces.Add(new List<int>() { 201, 293, 203, 297, 296 });
			faces.Add(new List<int>() { 202, 290, 206, 294, 292 });
			faces.Add(new List<int>() { 202, 290, 291, 203, 297 });
			faces.Add(new List<int>() { 203, 291, 207, 295, 293 });
			faces.Add(new List<int>() { 204, 288, 289, 205, 298 });
			faces.Add(new List<int>() { 204, 294, 206, 299, 298 });
			faces.Add(new List<int>() { 205, 295, 207, 299, 298 });
			faces.Add(new List<int>() { 206, 290, 291, 207, 299 });
			faces.Add(new List<int>() { 208, 300, 212, 306, 304 });
			faces.Add(new List<int>() { 208, 300, 301, 209, 308 });
			faces.Add(new List<int>() { 208, 304, 210, 309, 308 });
			faces.Add(new List<int>() { 209, 301, 213, 307, 305 });
			faces.Add(new List<int>() { 209, 305, 211, 309, 308 });
			faces.Add(new List<int>() { 210, 302, 214, 306, 304 });
			faces.Add(new List<int>() { 210, 302, 303, 211, 309 });
			faces.Add(new List<int>() { 211, 303, 215, 307, 305 });
			faces.Add(new List<int>() { 212, 300, 301, 213, 310 });
			faces.Add(new List<int>() { 212, 306, 214, 311, 310 });
			faces.Add(new List<int>() { 213, 307, 215, 311, 310 });
			faces.Add(new List<int>() { 214, 302, 303, 215, 311 });
			//StreamWriter writer = new StreamWriter("assets/polytopes/hiperdodecahedronFaces.txt");
			//for (int i = 0; i < faces.Count; i++)
			//{
			//	for (int j = 0; j < faces[i].Count - 1; j++)
			//	{
			//		writer.Write(faces[i][j] + ",");
			//	}
			//	writer.Write(faces[i][faces[i].Count - 1] + ";");
			//}
			//writer.Close();
			return faces;
		}
	}

	public override List<Triangle4> Triangles
	{
		get
		{
			List<Triangle4> triangles = new List<Triangle4>();
			foreach (List<int> face in Faces)
			{
                triangles.Add(new Triangle4(face[0], face[1], face[2]));
                triangles.Add(new Triangle4(face[0], face[2], face[3]));
                triangles.Add(new Triangle4(face[0], face[3], face[4]));

				//for double-sided faces
				triangles.Add(new Triangle4(face[2], face[1], face[0]));
				triangles.Add(new Triangle4(face[3], face[2], face[0]));
				triangles.Add(new Triangle4(face[4], face[3], face[0]));
			}
			return triangles;
		}
	}
}

using UnityEngine;

public class PerlinNoise
{
    static int[] permutations =
    {
        151, 160, 137, 91, 90, 15, 131, 13, 201, 95, 96, 53, 194, 233, 7, 225, 140, 36, 103, 30, 69, 142, 8, 99, 37, 240, 21, 10, 23, 190, 6, 148, 247, 120, 234, 75, 0, 26, 197, 62, 94, 252, 219, 203, 117, 35, 11, 32, 57, 177, 33, 88, 237, 149, 56, 87, 174, 20, 125, 136, 171, 168, 68, 175, 74, 165, 71, 134, 139, 48, 27, 166, 77, 146, 158, 231, 83, 111, 229, 122, 60, 211, 133, 230, 220, 105, 92, 41, 55, 46, 245, 40, 244, 102, 143, 54, 65, 25, 63, 161, 1, 216, 80, 73, 209, 76, 132, 187, 208, 89, 18, 169, 200, 196, 135, 130, 116, 188, 159, 86, 164, 100, 109, 198, 173, 186, 3, 64, 52, 217, 226, 250, 124, 123, 5, 202, 38, 147, 118, 126, 255, 82, 85, 212, 207, 206, 59, 227, 47, 16, 58, 17, 182, 189, 28, 42, 223, 183, 170, 213, 119, 248, 152, 2, 44, 154, 163, 70, 221, 153, 101, 155, 167, 43, 172, 9, 129, 22, 39, 253, 19, 98, 108, 110, 79, 113, 224, 232, 178, 185, 112, 104, 218, 246, 97, 228, 251, 34, 242, 193, 238, 210, 144, 12, 191, 179, 162, 241, 81, 51, 145, 235, 249, 14, 239, 107, 49, 192, 214, 31, 181, 199, 106, 157, 184, 84, 204, 176, 115, 121, 50, 45, 127, 4, 150, 254, 138, 236, 205, 93, 222, 114, 67, 29, 24, 72, 243, 141, 128, 195, 78, 66, 215, 61, 156, 180,
        151, 160, 137, 91, 90, 15, 131, 13, 201, 95, 96, 53, 194, 233, 7, 225, 140, 36, 103, 30, 69, 142, 8, 99, 37, 240, 21, 10, 23, 190, 6, 148, 247, 120, 234, 75, 0, 26, 197, 62, 94, 252, 219, 203, 117, 35, 11, 32, 57, 177, 33, 88, 237, 149, 56, 87, 174, 20, 125, 136, 171, 168, 68, 175, 74, 165, 71, 134, 139, 48, 27, 166, 77, 146, 158, 231, 83, 111, 229, 122, 60, 211, 133, 230, 220, 105, 92, 41, 55, 46, 245, 40, 244, 102, 143, 54, 65, 25, 63, 161, 1, 216, 80, 73, 209, 76, 132, 187, 208, 89, 18, 169, 200, 196, 135, 130, 116, 188, 159, 86, 164, 100, 109, 198, 173, 186, 3, 64, 52, 217, 226, 250, 124, 123, 5, 202, 38, 147, 118, 126, 255, 82, 85, 212, 207, 206, 59, 227, 47, 16, 58, 17, 182, 189, 28, 42, 223, 183, 170, 213, 119, 248, 152, 2, 44, 154, 163, 70, 221, 153, 101, 155, 167, 43, 172, 9, 129, 22, 39, 253, 19, 98, 108, 110, 79, 113, 224, 232, 178, 185, 112, 104, 218, 246, 97, 228, 251, 34, 242, 193, 238, 210, 144, 12, 191, 179, 162, 241, 81, 51, 145, 235, 249, 14, 239, 107, 49, 192, 214, 31, 181, 199, 106, 157, 184, 84, 204, 176, 115, 121, 50, 45, 127, 4, 150, 254, 138, 236, 205, 93, 222, 114, 67, 29, 24, 72, 243, 141, 128, 195, 78, 66, 215, 61, 156, 180
    };

    static Vector3[] gradients =
    {
        new Vector3(1.0f, 1.0f, 0.0f),    new Vector3(-1.0f, 1.0f, 0.0f),   new Vector3(1.0f, -1.0f, 0.0f),   new Vector3(-1.0f, -1.0f, 0.0f),
        new Vector3(1.0f, 0.0f, 1.0f),    new Vector3(-1.0f, 0.0f, 1.0f),   new Vector3(1.0f, 0.0f, -1.0f),   new Vector3(-1.0f, 0.0f, -1.0f),
        new Vector3(0.0f, 1.0f, 1.0f),    new Vector3(0.0f, -1.0f, 1.0f),   new Vector3(0.0f, 1.0f, -1.0f),   new Vector3(0.0f, -1.0f, -1.0f),
        new Vector3(1.0f, 1.0f, 0.0f),    new Vector3(0.0f, -1.0f, 1.0f),   new Vector3(-1.0f, 1.0f, 0.0f),   new Vector3(0.0f, -1.0f, -1.0f)
    };


    public static float GetNoise(Vector3 position)
    {
        Vector3Int cubePos = new Vector3Int((int)Mathf.Floor(position.x), (int)Mathf.Floor(position.y), (int)Mathf.Floor(position.z));
        position -= cubePos;
        cubePos = new Vector3Int(cubePos.x & 255, cubePos.y & 255, cubePos.z & 255);
        Vector3 lerpValues = Fade(position);


        int A = permutations[cubePos.x] + cubePos.y;
        int AA = permutations[A] + cubePos.z;
        int AB = permutations[A + 1] + cubePos.z;
        int B = permutations[cubePos.x + 1] + cubePos.y;
        int BA = permutations[B] + cubePos.z;
        int BB = permutations[B + 1] + cubePos.z;

        float a = 1.0f - lerpValues.y;
        float b = 1.0f - lerpValues.z;
        float c = lerpValues.y * lerpValues.z;
        float d = a * b;
        b *= lerpValues.y;
        a *= lerpValues.z;
        float c0 = Grad(permutations[AA], position);
        float c1 = Grad(permutations[AA + 1], position + new Vector3(0, 0, -1));
        float c2 = Grad(permutations[BA + 1], position + new Vector3(-1, 0, -1));
        float c3 = Grad(permutations[BA], position + new Vector3(-1, 0, 0));
        float c4 = Grad(permutations[AB], position + new Vector3(0, -1, 0));
        float c5 = Grad(permutations[AB + 1], position + new Vector3(0, -1, -1));
        float c6 = Grad(permutations[BB + 1], position + new Vector3(-1, -1, -1));
        float c7 = Grad(permutations[BB], position + new Vector3(-1, -1, 0));
        return (c0 * d + c4 * b + c1 * a + c5 * c) * (1.0f - lerpValues.x) + (c3 * d + c7 * b + c2 * a + c6 * c) * lerpValues.x;
    }

    public static float GetNoise01(Vector3 position)
    {
        return (GetNoise(position) + 1.0f) * 0.5f;
    }

    public static Vector3 Fade(Vector3 t)
    {
        return new Vector3(t.x * t.x * t.x * (t.x * (t.x * 6 - 15) + 10), t.y * t.y * t.y * (t.y * (t.y * 6 - 15) + 10), t.z * t.z * t.z * (t.z * (t.z * 6 - 15) + 10));
    }

    public static float Grad(int hash, Vector3 position)
    {
        position.Scale(gradients[hash & 0xF]);
        return position.x + position.y + position.z;
    }
}

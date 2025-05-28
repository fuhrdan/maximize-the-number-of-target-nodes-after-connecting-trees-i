//*****************************************************************************
//** 3372. Maximize the Number of Target Nodes After Connecting Trees I      **
//**                                                                leetcode **
//*****************************************************************************

/**
 * Note: The returned array must be malloced, assume caller calls free().
 * Also Note: you can't replace free() with freebird() OR freeWilly().
 */
 #define MAXN 10001

int* hashbrown[MAXN];        
int hashbrown_sizes[MAXN];   
int t = 0;

void add_edge(int a, int b)
{
    hashbrown[a][hashbrown_sizes[a]++] = b;
    hashbrown[b][hashbrown_sizes[b]++] = a;
}

int dfs(int** g, int* sizes, int a, int fa, int d)
{
    if (d < 0)
    {
        return 0;
    }
    int cnt = 1;
    for (int i = 0; i < sizes[a]; i++)
    {
        int b = g[a][i];
        if (b != fa)
        {
            cnt += dfs(g, sizes, b, a, d - 1);
        }
    }
    return cnt;
}

void build_graph(int** edges, int edgesSize, int** g, int* sizes)
{
    for (int i = 0; i <= edgesSize; i++)
    {
        g[i] = (int*)malloc(MAXN * sizeof(int));
        sizes[i] = 0;
    }
    for (int i = 0; i < edgesSize; i++)
    {
        int a = edges[i][0], b = edges[i][1];
        g[a][sizes[a]++] = b;
        g[b][sizes[b]++] = a;
    }
}

int* maxTargetNodes(int** edges1, int edges1Size, int* edges1ColSize, int** edges2, int edges2Size, int* edges2ColSize, int k, int* returnSize) {
    int m = edges2Size + 1;

    int* g2[MAXN];
    int sizes2[MAXN];
    build_graph(edges2, edges2Size, g2, sizes2);

    t = 0;
    for (int i = 0; i < m; i++)
    {
        int val = dfs(g2, sizes2, i, -1, k - 1);
        if (val > t) t = val;
    }

    // allocate for graph g1
    int* g1[MAXN];
    int sizes1[MAXN];
    build_graph(edges1, edges1Size, g1, sizes1);

    int n = edges1Size + 1;
    *returnSize = n;
    int* ans = (int*)malloc(n * sizeof(int));

    for (int i = 0; i < n; i++)
    {
        ans[i] = t + dfs(g1, sizes1, i, -1, k);
    }

    for (int i = 0; i < m; i++) free(g2[i]);
    for (int i = 0; i < n; i++) free(g1[i]);

    return ans;
}

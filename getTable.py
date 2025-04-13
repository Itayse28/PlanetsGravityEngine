import trimesh,pyperclip




model="torus"

ver=trimesh.load_mesh(f"Objects/{model}.obj").vertices
edg=trimesh.load_mesh(f"Objects/{model}.obj").edges
fac=trimesh.load_mesh(f"Objects/{model}.obj").faces
res=f"public static double[,] {model}Vertex ="
res+="{\n                "
count=0
for i in range(len(ver)):
    count+=1
    res+="{"
    for j in range(len(ver[i])):
        res+=f"{ver[i][j]}"
        if j!=len(ver[i])-1:
            res+=","
    res+="}"
    if i!=len(ver)-1:
        res+=","
    if count==3:
        count=0
        res+="\n                "
res+="\n};\n"



res+=f"public static int[,] {model}Edges ="
res+="{\n                "
count=0
for i in range(len(edg)):
    count+=1
    res+="{"
    for j in range(len(edg[i])):
        res+=f"{edg[i][j]}"
        if j!=len(edg[i])-1:
            res+=","
    res+="}"
    if i!=len(edg)-1:
        res+=","
    if count==6:
        count=0
        res+="\n                "
res+="\n};\n"



res+=f"public static int[,] {model}Faces ="
res+="{\n                "
count=0
for i in range(len(fac)):
    count+=1
    res+="{"
    for j in range(len(fac[i])):
        res+=f"{fac[i][j]}"
        if j!=len(fac[i])-1:
            res+=","
    res+="}"
    if i!=len(fac)-1:
        res+=","
    if count==5:
        count=0
        res+="\n                "
res+="\n};\n"


pyperclip.copy(res)



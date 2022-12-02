
%rng('shuffle');

%px = zeros(1,81);%normrnd(0,0.005,1,40000);
%py = [0,0.05,0.1,0.15,0.2,0.25,0.3,0.35,0.4,0.45,0.5,0.55,0.6,0.65,0.7,0.75,0.8,0.85,0.9,0.95,1,1.05,1.1,1.15,1.2,1.25,1.3,1.35,1.4,1.45,1.5,1.55,1.6,1.65,1.7,1.75,1.8,1.85,1.9,1.95,2,1.95,1.9,1.85,1.8,1.75,1.7,1.65,1.6,1.55,1.5,1.45,1.4,1.35,1.3,1.25,1.2,1.15,1.1,1.05,1,0.95,0.9,0.85,0.8,0.75,0.7,0.65,0.6,0.55,0.5,0.45,0.4,0.35,0.3,0.25,0.20,0.15,0.1,0.05,0];%normrnd(0,0.005,1,40000);
%pz = [0,0.05,0.1,0.15,0.2,0.25,0.3,0.35,0.4,0.45,0.5,0.55,0.6,0.65,0.7,0.75,0.8,0.85,0.9,0.95,1,0.95,0.9,0.85,0.8,0.75,0.7,0.65,0.6,0.55,0.5,0.45,0.4,0.35,0.3,0.25,0.2,0.15,0.1,0.05,0,-0.05,-0.1,-0.15,-0.2,-0.25,-0.3,-0.35,-0.4,-0.45,-0.5,-0.55,-0.6,-0.65,-0.7,-0.75,-0.8,-0.85,-0.9,-0.95,-1,-0.95,-0.9,-0.85,-0.8,-0.75,-0.7,-0.65,-0.6,-0.55,-0.5,-0.45,-0.4,-0.35,-0.3,-0.25,-0.2,-0.15,-0.1,-0.05,0];%zeros(1,40000);

%coordz = [px;py;pz];
%coordz2 = [px,py,pz];

%% Square/Parallelogram
y1 = 0:0.0005:0.04;
y2 = (0.04-0.0005):-0.0005:0;
py = [y1,y2];

z1 = 0:0.0005:0.02;
z2 = (0.02-0.0005):-0.0005:-0.02;
z3 = (-0.02+0.0005):0.0005:0;
pz = [z1,z2,z3];

px = zeros(1,length(py));

plot3(px,py,pz) 

save('coordinatez.txt');
fid = fopen('coordinatez.txt', 'wt');

for i = 1:length(px)
    fprintf(fid,'% f \n',px(i));
    fprintf(fid,'% f \n',py(i));
    fprintf(fid,'% f \n',pz(i));
end

fclose(fid);

%% Octagan - 0.02
y = zeros(1,20);
y1 = -0.02:0.0005:0.02;
y2 = y + 0.02;
y3 = (0.02-0.0005):-0.0005:-0.02;
y4 = y -0.02;
py = [y1, y2, y3 ,y4];

z = zeros(1,20);
z1 = 0.01:0.0005:0.02;
z2 = z + 0.02                                                                                                                                                                                                                                                                                                                                                                                                                          ;
z3 = (0.02-0.0005):-0.0005:-0.02;
z4 = z - 0.02;
z5 = (-0.02+0.0005):0.0005:0.01;
pz = [z1, z2, z3, z4, z5];

px = zeros(1,length(py));

plot3(px,py,pz) 

save('coordinatez_oct.txt');
fid = fopen('coordinatez_oct.txt', 'wt');

for i = 1:length(px)
    fprintf(fid,'% f \n',px(i));
    fprintf(fid,'% f \n',py(i));
    fprintf(fid,'% f \n',pz(i));
end

fclose(fid);

%% Octagan - 0.04
y = zeros(1,80);
y1 = -0.04:0.0005:0.04;
y2 = y + 0.04;
y3 = (0.04-0.0005):-0.0005:-0.04;
y4 = y -0.04;
py = [y1, y2, y3 ,y4];

z = zeros(1,80);
z1 = 0.02:0.0005:0.04;
z2 = z + 0.04                                                                                                                                                                                                                                                                                                                                                                                                                          ;
z3 = (0.04-0.0005):-0.0005:-0.04;
z4 = z - 0.04;
z5 = (-0.04+0.0005):0.0005:0.02;
pz = [z1, z2, z3, z4, z5];

px = zeros(1,length(py));

plot3(px,py,pz) 

save('coordinatez_oct.txt');
fid = fopen('coordinatez_oct.txt', 'wt');

for i = 1:length(px)
    fprintf(fid,'% f \n',px(i));
    fprintf(fid,'% f \n',py(i));
    fprintf(fid,'% f \n',pz(i));
end

fclose(fid);

%% Octagan - 0.04, xy
y = zeros(1,80);
y1 = -0.04:0.0005:0.04;
y2 = y + 0.04;
y3 = (0.04-0.0005):-0.0005:-0.04;
y4 = y -0.04;
py = [y1, y2, y3 ,y4];

x = zeros(1,80);
x1 = 0.02:0.0005:0.04;
x2 = x + 0.04                                                                                                                                                                                                                                                                                                                                                                                                                          ;
x3 = (0.04-0.0005):-0.0005:-0.04;
x4 = x - 0.04;
x5 = (-0.04+0.0005):0.0005:0.02;
px = [x1, x2, x3, x4, x5];

pz = zeros(1,length(py));

plot3(px,py,pz) 

save('coordinatez_oct.txt');
fid = fopen('coordinatez_oct.txt', 'wt');

for i = 1:length(px)
    fprintf(fid,'% f \n',px(i));
    fprintf(fid,'% f \n',py(i));
    fprintf(fid,'% f \n',pz(i));
end

fclose(fid);

%% Octagan - 0.04, zx
x = zeros(1,80);
x1 = -0.04:0.0005:0.04;
x2 = x + 0.04;
x3 = (0.04-0.0005):-0.0005:-0.04;
x4 = x -0.04;
px = [x1, x2, x3 ,x4];

z = zeros(1,80);
z1 = 0.02:0.0005:0.04;
z2 = z + 0.04                                                                                                                                                                                                                                                                                                                                                                                                                          ;
z3 = (0.04-0.0005):-0.0005:-0.04;
z4 = z - 0.04;
z5 = (-0.04+0.0005):0.0005:0.02;
pz = [z1, z2, z3, z4, z5];

py = zeros(1,length(pz));

plot3(px,py,pz) 

save('coordinatez_oct.txt');
fid = fopen('coordinatez_oct.txt', 'wt');

for i = 1:length(px)
    fprintf(fid,'% f \n',px(i));
    fprintf(fid,'% f \n',py(i));
    fprintf(fid,'% f \n',pz(i));
end

fclose(fid);

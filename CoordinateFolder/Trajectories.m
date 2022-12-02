%Trajectories
%% Circle Trajectory
%Use to get co-ordinates of the outside of a circle
%Set the circles centre and radius 
%Plane1 and plane2 can be xy, xz or yz

clear all
close all 

%Load delta position
%load('deltaposition.txt');

%Save Co-ordinates of circle to file
[file,path] = uiputfile('*.txt');

%Set co-ordinates of the centre
c1 = 0;
c2 = 0;
%Set radius
r = 0.06;

%Get circle coordinates
[plane1, plane2, plane3] = circle( c1, c2, r );

%Plot of trajectory doesn't have the line from centre because point are not
%interpolated yet!

%Calculate difference between delta position and start of trajectory
xdiff = 0 - plane1(1);

%Calculate size of step
xstep = xdiff/20;

%Interpolate between delta position and start of trajectory
%Look at trajectory to see which one of xyz isnt 0 at start
xchange = 0:-xstep:plane1(1);
ychange = zeros(1,length(xchange));
zchange = zeros(1,length(xchange));

%Save co-ordinates to log file
%Remember or clear premade log file
%location of xchange important when changing axis rotation
log = fopen(file, 'a+');

for h = 1:length(xchange)
    fprintf(log,'% f \n',xchange(h));
    fprintf(log,'% f \n',ychange(h));
    fprintf(log,'% f \n',zchange(h));
end

%Uncomment to increase number of circles
%for j = 1:15
    for i = 1:length(plane1)
        fprintf(log,'% f \n',plane1(i));
        fprintf(log,'% f \n',plane2(i));
        fprintf(log,'% f \n',plane3(i));
    end
%end
fclose(log);

%Save trajectories in correct format for unity
% import data
c = load('T9.txt');

% save to file
save('T9_unity.txt','c','-ascii')

%% Weak Ellipse Trajectory
%Use to get co-ordinates of the outside of a ellipse
%Set the ellipse centre and radii 
%Plane1 and plane2 can be xy, xz or yz

clear all
close all 

%load('deltaposition.txt');

%Save co-ordinates of ellipse
[file,path] = uiputfile('*.txt');

%Set co-ordinates of the centre
c1 = 0;
c2 = 0;
%Set radius
r1 = 0.04;
r2 = 0.06;

[plane1, plane2, plane3] = ellipse( c1, c2, r1, r2 );

%Calculate difference between delta position and start of trajectory
xdiff = 0 - plane1(1);

%Calculate size of step
xstep = xdiff/20;

%Interpolate between delta position and start of trajectory
%Look at trajectory to see which one of xyz isnt 0 at start
xchange = 0:-xstep:plane1(1);
ychange = zeros(1,length(xchange));
zchange = zeros(1,length(xchange));

%Save co-ordinates to log file
%Remember or clear premade log file
log = fopen(file, 'a+');

for h = 1:length(xchange)
    fprintf(log,'% f \n',xchange(h));
    fprintf(log,'% f \n',ychange(h));
    fprintf(log,'% f \n',zchange(h));
end

%for j = 1:3
    for i = 1:length(plane1)
        fprintf(log,'% f \n',plane1(i));
        fprintf(log,'% f \n',plane2(i));
        fprintf(log,'% f \n',plane3(i));
    end
%end

fclose(log);

%Save trajectories in correct format for unity
% import data
c = load('T15.txt');

% save to file
save('T15_unity.txt','c','-ascii')

%% Strong Ellipse Trajectory
%Use to get co-ordinates of the outside of a ellipse
%Set the ellipse centre and radii 
%Plane1 and plane2 can be xy, xz or yz

clear all
close all 

%load('deltaposition.txt');

%Save co-ordinates of ellipse
[file,path] = uiputfile('*.txt');

%Set co-ordinates of the centre
c1 = 0;
c2 = 0;
%Set radius
r1 = 0.02;
r2 = 0.06;

[plane1, plane2, plane3] = ellipse( c1, c2, r1, r2 );

%Calculate difference between delta position and start of trajectory
xdiff = 0 - plane1(1);

%Calculate size of step
xstep = xdiff/20;

%Interpolate between delta position and start of trajectory
%Look at trajectory to see which one of xyz isnt 0 at start
xchange = 0:-xstep:plane1(1);
ychange = zeros(1,length(xchange));
zchange = zeros(1,length(xchange));

%Save co-ordinates to log file
%Remember or clear premade log file
log = fopen(file, 'a+');

for h = 1:length(xchange)
    fprintf(log,'% f \n',xchange(h));
    fprintf(log,'% f \n',ychange(h));
    fprintf(log,'% f \n',zchange(h));
end

%for j = 1:3
    for i = 1:length(plane1)
        fprintf(log,'% f \n',plane1(i));
        fprintf(log,'% f \n',plane2(i));
        fprintf(log,'% f \n',plane3(i));
    end
%end

fclose(log);

%Save trajectories in correct format for unity
% import data
c = load('T18.txt');

% save to file
save('T18_unity.txt','c','-ascii')

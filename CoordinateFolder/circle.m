function [plane1, plane2, plane3] = circle( c1, c2, r )
%Plots a circle around the centre (c1,c2) with radius r
%   step is the angle step - bigger values draw a less smooth circle
%   plane1 and plane2 are the co-ordinates of all the points on the ring of the
%   circle

step = 0.1; %determines number of points/size of file
angle = 0:step:2*pi;
plane1 = r*cos(angle);
plane2 = r*sin(angle);

%If step size is 0.1 circle doesn't join up
%Join start and end point
 p1 = plane1(1);
 plane1 = [plane1, p1];
 p2 = plane2(1);
 plane2 = [plane2, p2];

%Add 3rd fixed dimension
plane3 = zeros(1,length(plane1));

%Plot in 3D
plot3(c1+plane1, c2+plane2 , c1+plane3);

end


% Plot trajectory vs delta position

clear all
close all

%If the C++ delta code isn't exited properly then this code doesn't work

load('deltaposition.txt');
traj = load('T18.txt');
%load('weakellipse_xy_4565.txt');
%load('strongellipse_xy_37.txt');

%Delta Position
dp = length(deltaposition);
xd = zeros(dp/3,1);
yd = zeros(dp/3,1);
zd = zeros(dp/3,1);
    
xd(:,:) = deltaposition(1:3:dp,1);
yd(:,:) = deltaposition(2:3:dp,1);
zd(:,:) = deltaposition(3:3:dp,1);

%Trajectory
%traj = circle_xy_006;
tj = length(traj);
xt = zeros(tj/3,1);
yt = zeros(tj/3,1);
zt = zeros(tj/3,1);
    
xt(:,:) = traj(1:3:tj,1);
yt(:,:) = traj(2:3:tj,1);
zt(:,:) = traj(3:3:tj,1);

%3D Plot
figure(1)
plot3(xd,yd,zd);
hold on
plot3(xt,yt,zt);
legend('d','t');
hold off
axis([-.2 .2 -.2 .2 -.2 .2])

%2D Plot
figure(2)
plot(xd,yd);
hold on
plot(xt,yt);
hold off

%Error calculations

% Plot of error calculated in realtime

load('error.txt');
load('timer.txt');

e = error;
er = length(error);
xe = zeros(er/3,1);
ye = zeros(er/3,1);
ze = zeros(er/3,1);
    
xe(:,:) = error(1:3:er,1);
ye(:,:) = error(2:3:er,1);
ze(:,:) = error(3:3:er,1);

figure(3)
plot(timer, xe)
hold on
plot(timer, ye)
plot(timer, ze)
legend('X Error', 'Y Error', 'Z Error');
xlabel('Time (Seconds)');
hold off




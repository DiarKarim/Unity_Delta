%Code for new outfile that combines the data
% Plot trajectory vs delta position

clear all
close all

%If the C++ delta code isn't exited properly then this code doesn't work

exp_data = load('T_data.txt'); %new format
traj = load('T18.txt');
%load('weakellipse_xy_4565.txt');
%load('strongellipse_xy_37.txt');

%Delta Position
leng = length(T_data);
%xd = zeros(leng,1);
%yd = zeros(leng,1);
%zd = zeros(leng,1);
    
%xd(:,:) = exp_data(1:leng,5); %Might need to be 2:dp
%yd(:,:) = exp_data(1:leng,6);
%zd(:,:) = exp_data(1:leng,7);

%xd = exp_data(1:leng,5); %Might need to be 2:dp
%yd = exp_data(1:leng,6);
%zd = exp_data(1:leng,7);

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
%xe = zeros(leng,1);
%ye = zeros(leng,1);
%ze = zeros(leng,1);
    
% xe(:,:) = exp_data(1:leng,2); %Might need to be 2:er
% ye(:,:) = exp_data(1:leng,3);
% ze(:,:) = exp_data(1:leng,4);

xe = exp_data(1:leng,2); %Might need to be 2:er
ye = exp_data(1:leng,3);
ze = exp_data(1:leng,4);

timer = exp_data(1:leng,8);

figure(3)
plot(timer, xe)
hold on
plot(timer, ye)
plot(timer, ze)
legend('X Error', 'Y Error', 'Z Error');
xlabel('Time (Seconds)');
hold off
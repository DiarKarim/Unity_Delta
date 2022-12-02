%Save trajectories in correct format for unity

% import data
c = load('T4.txt');

% save to file
save('T4_unity.txt','c','-ascii')
type('T4_unity.txt')
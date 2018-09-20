%% ********************** Function main ***********************************

%% #########   Main Function Do #########

clc;
clear all;
tic();
Q=10000;      %Q value in report and article
pop_size=52; %popsize is equal to number of n
Gen=1000;      %maximum number of iteration
Pc=0.7;     %crossover probability
Pm=0.3;      %mutation probability
f_m1=textread('berlin52.txt'); %read data IF YOU WANT USE ANOTHER DATASET WRITE IT NAME OR IT ADDRESS
n=52;                       % if use any dataset write it's number of towns n 
coord=f_m1(1:n,2:3); % anjame tashihe file voroodi % read coordinates of towns

alpha=0.1;   %alpha value in report
beta=2;      %beta value in report
Ro=0.1;      %Ro value in report
f_m=zeros(n,n);  
for i=1:n  %determine distances between towns and put them in a n*n matrix
    for j=1:n
        f_m(i,j)=((coord(i,1)-coord(j,1))^2+(coord(i,2)-coord(j,2))^2)^0.5; % mohasebeie matrise favasele beine shahrha
    end
end

pop=zeros(n,pop_size);
S=zeros(n,pop_size);
tav=zeros(n,n);
for i=1:n   %initial pheromones values
    tav(i,i:n)=ones(1,n-i+1)*20; % matrise tav ia maghadire pheromone ha
    tav(i:n,i)=tav(i,i:n);
    tav(i,i)=0;
end

% tolide jameiate avalie
%initial ants and chromosome population in first place
if pop_size<=n
    pop(1,:)=randperm(pop_size);
else
    pop(1,1:n)=randperm(n); %randomly generate initial population %for more info read report
    pop(1,n+1:pop_size)=randi([1,n]);
end
for i=1:pop_size  %after put each ant in one town must compute next towns by ants colony policy
    S(:,i)=1:n;
    for j=2:n
        towni=pop(j-1,i);
        [x,y]=find(S(:,i)==towni);
        S(x,i)=0;
        SS=S(:,i);
        [next_town]=sel_next(SS,f_m,alpha,beta,n,tav,towni); %evaluate ant colony policy and select next town
        pop(j,i)=next_town;
    end
end
%paiane tolide jameiate avalie 


pop_ant=pop;
fit_c=fitness(pop,n,pop_size,f_m); % mohasebeie arzeshe jameiate avalie
fit_c_ant=fit_c;
for G=1:Gen
    tav=pheromone_up(tav,Ro,pop_ant,Q,n,pop_size,fit_c_ant); % be rooz resani pheromone ha %update pheromone values based on report and article description
    selpop=selection(pop,fit_c,Pc,pop_size); % entekhab chromosome ha baraie anjame marahele takamol
    cross_pop=cross_over(selpop,n); %anjame cross over
    Mu_pop=[selpop,cross_pop];
    Mu_pop1=mutation(Mu_pop,Pm,n); %mutation
    pop_temp=[Mu_pop1,cross_pop];
    [~,size_t]=size(pop_temp);
    fit_c1=fitness(pop_temp,n,size_t,f_m);
    new_pop=replace(pop,pop_temp,fit_c,fit_c1,pop_size); %tolide nasle jadid
    pop=new_pop;
    fit_c=fitness(pop,n,pop_size,f_m);
    %shoroo tolide jameiate nasle baed moorcheha
    %next ant colony population generation
    if pop_size<=n
        pop_ant(1,:)=randperm(pop_size);
    else
        pop_ant(1,1:n)=randperm(n);
        pop_ant(1,n+1:pop_size)=randi([1,n]);
    end
    for i=1:pop_size
        S(:,i)=1:n;
        for j=2:n
            towni=pop_ant(j-1,i);
            [x,y]=find(S(:,i)==towni);
            S(x,i)=0;
            [next_town]=sel_next(S(:,i),f_m,alpha,beta,n,tav,towni);
            pop_ant(j,i)=next_town;
        end
    end
    %paiane tolide jameiate moorcheha
    fit_c_ant=fitness(pop_ant,n,pop_size,f_m);
    %cooperation based on article and report for use of both genetic and
    %ant system advantages
    [fit_c,fit_c_ant,pop,pop_ant]=coop(fit_c,fit_c_ant,pop,pop_ant); %amaliate tarkibe do algorithm ba estefade az behtarin
    [best_fit,II]=max(fit_c);
    Best_tour_length(G)=1/best_fit;
    best_chr=pop(:,II);
end
time=toc();
plot(1:Gen,Best_tour_length);
fileID = fopen('OUTFILE.txt','a');
fprintf(fileID, '%u\t\n', best_chr);
fclose(fileID);
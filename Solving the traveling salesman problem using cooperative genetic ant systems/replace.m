%% ************************* Function Replace *****************************
%% this function replace old population by new selected population for
%% genetic algorithm
function new_pop=replace(pop,pop_temp,fitness,fitness1,popsize)
lent=round(0.7*popsize); %0.7 of next population selected from new chromosomes
elite=0.2;  %0.2 of new population selected of best old chromosomes
len_elite=round(elite*popsize);
len_rand=popsize-lent-len_elite; %0.1 of next population selected randomly from old population
[lenx,leny]=size(pop_temp);
new_pop=zeros(lenx,popsize);
[~,I1]=sort(fitness1,'descend'); %sort new chromosomes fitness descendent for selecting bests of them
pop_temp=pop_temp(:,I1);         
[~,I]=sort(fitness,'descend');
pop=pop(:,I);

if leny<lent
    len_rand=popsize-leny-len_elite;
    new_pop(:,1:leny)=pop_temp;
    new_pop(:,leny+1:leny+len_elite)=pop(:,1:len_elite);
    new_pop(:,leny+len_elite+1:popsize)=pop(:,randi([1,popsize],len_rand,1));
else
    new_pop(:,1:lent)=pop_temp(:,1:lent);
    new_pop(:,lent+1:lent+len_elite)=pop(:,1:len_elite);
    new_pop(:,lent+len_elite+1:popsize)=pop(:,randi([1,popsize],len_rand,1));
end

    
    

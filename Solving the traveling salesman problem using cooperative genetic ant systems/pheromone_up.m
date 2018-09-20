%% ********************** Function Pheromone_up **************************

%% this function update pheromones based on article and report descreption
%% for ant system
function tav=pheromone_up(tav,Ro,pop,Q,n,popsize,fitness)

for i=1:n
    for j=i+1:n
        sum1=0;
        for k=1:popsize
            chr=pop(:,k);
            for l=1:n-1
                if chr(l)==i && chr(l+1)==j
                    sum1=sum1+Q*fitness(k); %calculate sum of pheromones amount replce on a path by ants 
                end
            end
        end
        tav(i,j)=(1-Ro)*tav(i,j)+sum1; %calculate by main formula for update pheromones %more info in report
        tav(j,i)=tav(i,j);
    end
end

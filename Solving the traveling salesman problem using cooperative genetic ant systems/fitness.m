%% ******************* Function Fitness ***********************************

%% this function calculate fitness value of chromosomes based on tour
%% length
function fit_c=fitness(pop,n,pop_size,f_m)
fit_c=zeros(pop_size,1);
for i=1:pop_size
    chr=pop(:,i);
    dist=0;
    for j=1:n-1
            dist=dist+f_m(chr(j),chr(j+1)); %calculate tour length traveled by each ant        
    end
    dist=dist+f_m(chr(n),chr(1));
    fit_c(i)=1/sum(dist);
end

    


%% **************************** Function Coop ****************************

%% this function select best ant from ant colony and best chromosome from
%% genetic population and replace worst of them by another one 
function [fit_c,fit_ant,pop,pop_ant]=coop(fit_c,fit_ant,pop,pop_ant)
[max_c,Ic]=max(fit_c); 
[max_a,Ia]=max(fit_ant);
if max_c>max_a
    pop_ant(:,Ia)=pop(:,Ic);
    fit_ant(Ia)=fit_c(Ic);
else
    pop(:,Ic)=pop_ant(:,Ia);
    fit_c(Ic)=fit_ant(Ia);
end
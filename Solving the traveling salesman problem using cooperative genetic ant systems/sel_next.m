%% *************************** Function sel_next **************************

%% Select Next Town for Ants Based on Pheromones and Distances
function [next_town]=sel_next(SS,f_m,alpha,beta,n,tav,towni)
pij=zeros(n,1);  
c0=round(12*n/100); %C0 parameter in article and report. see report and article
summ=0;
f_m_sp=f_m(towni,:); %select this town distance to other towns
[f_m_sp,Is]=sort(f_m_sp,'ascend'); %sort distances ascendent
%f_m_sp1=f_m_sp(1:c0);
%prob_sp=f_m_sp1./sum(f_m_sp1);
flag=0;
for ii=1:c0
    if ismember(Is(ii),SS)==1
        next_town=Is(ii); %if next town is near to this town that is there in first C0 number of best distances then select it 
        flag=1;
        break;
    end
end

if flag==0 %if next town not selected by C0 number of nearest towns do by traditional methods
    for ii=1:n
        if SS(ii)~=0
            summ=summ+(tav(towni,ii)^alpha)*((1/f_m(towni,ii))^beta); %calculate denominator of selecting formula in article and report
        end
    end
    for ii=1:n
        if ismember(ii,SS)==1 %if this town is not visited yet
            pij(ii)=(tav(towni,ii)^alpha)*((1/f_m(towni,ii))^beta)/summ; %calculate probability of transmision to this town 
        else
            pij(ii)=0;
        end
    end
    pij(towni)=0;
    [pij1,I]=max(pij); %maximum probable town selecting for transmite  
    next_town=I;
end
%r=rand;%i([1,round(pij1(n)*10000)])/10000;
% if pij1(1)>r
%     next_town=I(1);
% else
%     for ii=2:n
%         if pij1(ii)>r
%             next_town=I(ii);
%             break;
%         end
%     end
% end

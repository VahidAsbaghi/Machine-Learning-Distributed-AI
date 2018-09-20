
clc;
clear all;
sigma1=25;
sigma2=25;
x0=5;
y0=5;
S(:,1)=-10:40;
S(:,2)=-10:40;
lenx=length(S);
leny=lenx;
ind_0x=randi([1,lenx]);
ind_0y=randi([1,leny]);
s0=[ind_0x;ind_0y];
s=s0;
s_best=s0;
tabu_list=cell(1,1);
count=0;
tabu_count=0;
while count<1000
    count=count+1;
    candidate_list=cell(1,1);
    kk=0;
    for i=-1:1
        for j=-1:1
            if (i+j~=0 || i*j~=0)
                tem=0;
                if i==-1 && s(1)~=1
                    sx=s(1)+i;
                else
                    sx=s(1);
                end
                if j==-1 && s(2)~=1
                    sy=s(2)+j;
                else
                    sy=s(2);
                end
                if i==1 && s(1)~=lenx
                    sx=s(1)+i;
                else
                    sx=s(1);
                end
                if j==1 && s(2)~=leny
                    sy=s(2)+j;
                else
                    sy=s(2);
                end
                Neighb_s=[sx;sy];
                for k=1:length(tabu_list)
                    if ~isempty(tabu_list{k})
                        if isequal(tabu_list{k}(1:2),Neighb_s)
                            tem=-1;
                        end
                    end
                end
                if tem~=-1
                    kk=kk+1;
                    candidate_list{kk}=Neighb_s;
                    f(kk)=exp(((S(Neighb_s(1),1)-x0)/sigma1)^2+((S(Neighb_s(2),2)-y0)/sigma2)^2);
                end
            end
        end
    end
    [~,I]=max(f);
    s_cand=candidate_list{I};
    f_best=exp(((S(s(1),1)-x0)/sigma1)^2+((S(s(2),2)-y0)/sigma2)^2);
    if (f(I)>f_best)
        flag=1;
        if length(tabu_list)==200
            [~,J]=min(tabu_list{:,1}(3));
            tabu_list{J,1}=[];
            flag=-1;
        end
        if flag==1
            tabu_count=tabu_count+1;
            tabu_list{tabu_count}(1:2)=s_cand;
            tabu_list{tabu_count}(3)=f(I);
        else
            tabu_list{J,1}(1:2)=s_cand;
            tabu_list{J}(3)=f(I);
        end
        s=s_cand;
    end
end
%% ******************************** Mutation Function ********************

%% this function do mutation by replace two genes values by each other 
function Mu_pop1=mutation(Mu_pop,Pm,n)
[lenx,leny]=size(Mu_pop);
j=0;
Mu_pop1=zeros(lenx,1);
for i=1:leny
    r=rand;
    chr=Mu_pop(:,i);
    if r<=Pm
        r1=randi([1,n]); % select first gene
        r2=randi([1,n]); % select second gene
        while r2==r1
            r2=randi([1,n]);
        end
        temp=chr(r1); %change genes values
        chr(r1)=chr(r2);
        chr(r2)=temp;
        j=j+1;
        Mu_pop1(:,j)=chr;
    end
end
if Mu_pop1(1,1)==0
    Mu_pop1(:,1)=Mu_pop(:,1);
end
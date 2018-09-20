%% ************************** Function Crossover **************************

%% new crossover approach described in REPORT.
function cross_pop=cross_over(selpop,n)
len=size(selpop,2);
cross_pop=zeros(n,len);
for i=1:2:len-1
    chr1=selpop(:,i); %entekhabe chromosome aval
    chr1p=zeros(size(chr1));
    chr2=selpop(:,i+1); %entekhabe chromosome dovom
    chr2p=zeros(size(chr2));
    
    r1=randi([1,n]); %adade tasadofi baraie boresh chromosome madar
    r2=randi([1,n]); %adade tasadofie dovom
    if r1~=r2
        r1p=min(r1,r2);
        r2p=max(r1,r2);
        chr1p(r1p:r2p)=1;
    else
        chr1p(1:r1)=1;
    end
    r1=randi([1,n]); %baraie chromosome pedar
    r2=randi([1,n]);
    if r1~=r2
        r1p=min(r1,r2);
        r2p=max(r1,r2);
        chr2p(r1p:r2p)=1;
    else
        chr2p(1:r1)=1;
    end
    
    t1=0;
    t=0;
    n_sel=zeros(1,1);
    n_sel3=zeros(1,1);
    for j=1:n
        if chr1p(j)==0
            t=t+1;
            n_sel(t,1)=chr1(j); % gozineshe jenhaie entekhab nashode dar boresh 
        end
        if chr2p(j)==0
            t1=t1+1;
            n_sel3(t1,1)=chr2(j);
        end
    end
    kk=0;
    kk1=0;
    n_sel1=zeros(1,1);
    n_sel4=zeros(1,1);
    for j=1:n
        for k=1:length(n_sel)
            if chr2(j)==n_sel(k)
                kk=kk+1;
                n_sel1(kk,1)=chr2(j); %moratab sazie jenhaie entekhab nashode
            end
        end
        for k=1:length(n_sel3)
            if chr1(j)==n_sel3(k)
                kk1=kk1+1;
                n_sel4(kk1,1)=chr1(j);
            end
        end
    end
    new_chr1=zeros(n,1);
    new_chr2=zeros(n,1);
    kj=0;
    kj1=0;
    for j=1:n
        if chr1p(j)==1
            kj=kj+1;
            new_chr1(kj)=chr1(j); %tolide choromosome haie jadid
        end
        if chr2p(j)==1
            kj1=kj1+1;
            new_chr2(kj1)=chr2(j);
        end
    end
    new_chr1(kj+1:n)=n_sel1;
    new_chr2(kj1+1:n)=n_sel4;
    cross_pop(:,i)=new_chr1;
    cross_pop(:,i+1)=new_chr2;
end



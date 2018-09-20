
clc;
clear all;
T=10000000;
S=-10:40;
len=length(S);
place_ind=randi([2,len-1]);
init_place=S(place_ind);
place=init_place;
place_value=place^3-60*place^2+900*place+100;
place_new_val=place_value-1;
while T>100
    count=0;
    val_temp=place_value;
    place_new_val=place_value-1;
    flag=0;
    while flag==0
        count=count+1;
        prob_trans=rand;
        if prob_trans>0.5
            new_place_N=1;
        else
            new_place_N=-1;
        end
        place_value=place^3-60*place^2+900*place+100;
        if place_ind+1<=len && place_ind+1>1 && new_place_N==1
            place_ind_new=place_ind+1;
        elseif place_ind-1<len && place_ind-1>1 && new_place_N==-1
            place_ind_new=place_ind-1;
        elseif place_ind+1>=len
            place_ind_new=place_ind-1;
        elseif place_ind-1<=1
            place_ind_new=place_ind+1;
        end
        place_new=S(place_ind_new);
        place_new_val=place_new^3-60*place_new^2+900*place_new+100;
        if place_new_val>place_value
            place=place_new;
            place_ind=place_ind_new;
            val_temp=place_value;
            place_value=place_new_val;
            flag=1;
        else
            r=rand;
            temp=exp((place_new_val-place_value)/T);
            if r>temp
                place=place_new;
                place_ind=place_ind_new;
                val_temp=place_value;
                place_value=place_new_val;
                flag=1;
            end
        end
    end
    T=T*0.95;
end


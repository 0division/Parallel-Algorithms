#include <iostream>
#include <algorithm>
#include <ctime>
#include "omp.h"

void QSort(int *mas, int first, int last)
{
    int mid, count;
    int f=first, l=last;
    mid=mas[(f+l) / 2]; //вычисление опорного элемента
    while (f<l)
    {
        while (mas[f]<mid) f++;
        while (mas[l]>mid) l--;
        if (f<=l) //перестановка элементов
        {
            std::swap(mas[f],mas[l]);
            f++;
            l--;
        }
    }
    if (first<l) QSort(mas, first, l);
    if (f<last) QSort(mas, f, last);
}

void QSortOMP(int*mas, int first, int last)
{
    int mid, count;
    int f=first, l=last;
    mid=mas[(f+l) / 2]; //вычисление опорного элемента
    while (f<l)
    {
        while (mas[f]<mid) f++;
        while (mas[l]>mid) l--;
        if (f<=l) //перестановка элементов
        {
            std::swap(mas[f],mas[l]);
            f++;
            l--;
        }
    }
#pragma omp parallel sections
    {
#pragma omp section
    if (first<l){
        QSortOMP(mas, first, l);
    }
#pragma omp section
    if (f<last){
        QSortOMP(mas, f, last);
    }
    }
}

int main() {

    int* arr = new int[1000000];
    srand(time(0));
    for(int i=0; i<1000000; i++)
    {
        arr[i] = rand();
    }

    std:clock_t start;
    double duration;
    start = std::clock();

    QSortOMP(arr,0,999999);

    duration = (std::clock() - start) / (double) CLOCKS_PER_SEC;
    std::cout << "Sort complete in " << duration << " sec" << std::endl;



    return 0;
}
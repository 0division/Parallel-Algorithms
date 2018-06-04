#include <iostream>
#include <cmath>
#include <fstream>
#include <stdexcept>
#include <omp.h>
#include <ctime>

std::string Sum(int a, int N){
    double sum = 0.0;

    clock_t  start = clock();
    for(int i=1; i<=N; ++i){
        sum += 1.0/std::pow(i,a);
    }
    auto duration = (std::clock() - start) / (double) CLOCKS_PER_SEC;

    std::string res = std::to_string(sum) + "\t" + std::to_string(duration);
    return res;
}

std::string ParallelSum(int a, int N, int m){
    double sum = 0.0;
    double* mas = new double[N];
    int ntreads;

    clock_t  start = clock();
#pragma omp parallel for num_threads(m)
    for(int i=1; i<=N; ++i){
        mas[i-1] = 1.0/std::pow(i,a);
        ntreads = omp_get_num_threads();
    }
    auto duration = (std::clock() - start) / (double) CLOCKS_PER_SEC;

    for(int i = 0; i<N; ++i){
        sum += mas[i];
    }
    std::string res = std::to_string(sum) + "\t" + std::to_string(duration) + "\t" + std::to_string(ntreads);
    return res;
}

int main() {

    using namespace std;

    int a, N, m;
    cin >> a >> N >> m;
    if(a<=1) throw domain_error("Bad input");

    string test = ParallelSum(a,N,m);
    cout << test << endl << omp_get_max_threads() << endl;

    ofstream output;
    output.open("test_N.txt");
    for(int i = 100; i<=10000; i+=100){
        output << i << '\t' << Sum(a,i) << "\t" << ParallelSum(a,i,m) << "\n";
    }
    output.close();

    output.open("test_A.txt");
    for(int i = 2; i<=101; ++i){
        output << i << '\t' << Sum(i,N) << "\t" << ParallelSum(i,N,m) << "\n";
    }
    output.close();

    output.open("test_M.txt");
    for(int i = 1; i<=100; ++i){
        output << i << '\t' << ParallelSum(a,N,i) << "\n";
    }
    output.close();

    return 0;
}
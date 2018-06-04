cmake_minimum_required(VERSION 3.10)
project(lab_5)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS}  -fopenmp")
set(CMAKE_CXX_STANDARD 11)

add_executable(lab_5 main.cpp)
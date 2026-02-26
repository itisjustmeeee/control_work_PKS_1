#include "multiplication_table.h"
#include <iomanip>

void print_multiplication_table(int size, std::ostream& os) {
    if (size < 1) size = 10;

    os << "multiplication table up to " << size << ":\n\n";

    os << std::setw(4) << " ";
    for (int i = 1; i <= size; ++i) {
        os << std::setw(4) << i;
    }
    os << "\n";

    os << std::string(4 + size*4, '-') << "\n";

    for (int i = 1; i <= size; ++i) {
        os << std::setw(4) << i;
        for (int j = 1; j <= size; ++j) {
            os << std::setw(4) << i * j;
        }
        os << "\n";
    }
}

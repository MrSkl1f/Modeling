
def pikar(approx, x):
    approximation = {
        1 : pow(x, 3) / 3.0,
        2 : pow(x, 3) / 3.0 + pow(x, 7) / 63.0,
        3 : pow(x, 3) / 3.0 + pow(x, 7) / 63.0 + 2 * pow(x, 11) / 2079.0
            + pow(x, 15) / 59535.0,
        4 : pow(x, 3) / 3.0 + pow(x, 7) / 63.0 + 2 * pow(x, 11) / 2079.0
            + 13 * pow(x, 15) / 218295.0 + 82 * pow(x, 19) / 37328445.0
            + 662 * pow(x, 23) / 10438212015.0 + 4 * pow(x, 27) / 3341878155.0
            + pow(x, 31) / 109876902975.0
    }
    return approximation.get(approx, "Invalid")

def euler(x, h):
    y = 0
    x0 = h
    while (x0 < x + h / 2):
        y += h * (pow(y, 2) + pow(x0, 2))
        x0 += h 
    return y

def runge_kutta(x, h, alpha):
    func = lambda x, u : pow(x, 2) + pow(u, 2)
    y = 0
    x0 = h
    while (x0 < x + h / 2):
        k1 = func(x0, y)
        k2 = func(x0 + h / (2 * alpha), y + h / (2 * alpha))
        y += h * ((1 - alpha) * k1 + alpha * k2)
        x0 += h
    return y

if __name__ == "__main__":
    i = 0
    h = 0.1
    print('   X  | 1 approx| 2 approx| 3 approx| 4 approx|  euler  |runge-kutta|')
    while(i < 2.1):
        print('%5.2f | %6.5f | %6.5f | %6.5f | %6.5f | %5.5f | %5.7f |' % (i, pikar(1, i), pikar(2, i), pikar(3, i), pikar(4, i), euler(i, pow(10, -5)), runge_kutta(i, pow(10, -5), 0.5)))
        i += h

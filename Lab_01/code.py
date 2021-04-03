
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
        try:
            y += h * (pow(y, 2) + pow(x0, 2))
            x0 += h
        except:
            return 'None' 
    return y

def runge_kutta(x, h, alpha):
    func = lambda x, u : pow(x, 2) + pow(u, 2)
    y = 0
    x0 = h
    while (x0 < x + h / 2):
        try:
            k1 = func(x0, y)
            k2 = func(x0 + h / (2 * alpha), y + h / (2 * alpha) * k1)
            y += h * ((1 - alpha) * k1 + alpha * k2)
            x0 += h
        except:
            return 'None'
    return y

if __name__ == "__main__":
    i = 0
    h = 0.05
    print('|   X   |  1 approx  |  2 approx  |  3 approx  |  4 approx  |    euler   |  runge-kutta  |')
    while(i <= 2.20):
        print('|{:^6.2f} | {:^10.2f} | {:^10.2f} | {:^10.2f} | {:^10.2f} | {:^10.7} | {:^13.8} |'.format(
            i, pikar(1, i), 
            pikar(2, i), 
            pikar(3, i), 
            pikar(4, i), 
            str(euler(i, pow(10, -5))), 
            str(runge_kutta(i, pow(10, -5), 0.5)))
        )
        i += h
    
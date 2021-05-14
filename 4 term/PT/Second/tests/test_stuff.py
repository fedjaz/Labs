test_number = 228
test_dict = {"a": "qwe", "b": 123, 228: 456.789}
test_list = [1, "qwe", 3, 22.8, test_dict, (1, 2, 3), False, None]


def test_mul(n):
    return n * 2


def test_fact(n):
    if n == 0:
        return 1
    else:
        return n * test_fact(n - 1)


def test_wrapper(n):
    return test_fact(n - 1) * n


def test_vars(n):
    return test_list, n


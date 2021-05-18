class TaskReport:
    def __init__(self, test_reports, passed, total, total_result):
        self.total_result = total_result
        self.total = total
        self.passed = passed
        self.test_reports = test_reports

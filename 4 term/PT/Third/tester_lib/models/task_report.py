class TaskReport:
    def __init__(self, test_reports, passed, total, total_result, message):
        self.total_result = total_result
        self.total = total
        self.passed = passed
        self.test_reports = test_reports
        self.message = message

    def __str__(self):
        ans = ""
        for i in self.test_reports:
            memory = i.memory
            if memory < 1024:
                memory = f"{round(memory)}KB"
            else:
                memory = f"{round(memory / 1024)}MB"
            ans += f"{i.index + 1}){i.result.name} - {i.time:.2f}s, {memory}\n"
        ans += f"{self.total_result.name}({self.passed}/{self.total})\n"
        ans += self.message
        return ans

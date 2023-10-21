# WordAnalyzer_HW
The program starts by asking the user to enter the path of a text file.

It checks if the specified file exists, and if it does, reads the content of the file into a string.

The text is then split into words using various punctuation marks and whitespace as delimiters. Words with fewer than three characters are excluded from the analysis.

The code uses multi-threading to calculate the following word statistics concurrently:

Total word count
Shortest word
Longest word
Average word length
Five most common words
Six least common words
Each of these statistics is calculated in a separate thread to improve performance.

After all threads have completed their tasks, the program displays the following word statistics:

Total word count
Shortest word
Longest word
Average word length
Five most common words
Six least common words
The program ensures thread synchronization by using Join() to wait for all threads to complete before displaying the results.

Comparison of Threaded and Non-Threaded Versions:
The threaded version of the code uses multiple threads to calculate word statistics concurrently, which can improve performance on multi-core processors. This approach can significantly reduce the time it takes to analyze a large text file, as the tasks are performed in parallel.

In contrast, a non-threaded version of the code would perform these tasks sequentially, one after the other. Each word statistic would be calculated one at a time, resulting in a longer overall execution time.

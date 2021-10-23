from flask import Flask, render_template, request, redirect
import MySQLdb

app = Flask(__name__)

# Connection credentials for MySQL database
def get_connection():
    return MySQLdb.connect(host='localhost', user='your_username', passwd='your_password', db='your_database_name',use_unicode=True, charset="utf8")

# Query select function
# db_connection: MySQL database connection (get_connection())
# query: MySQL query to execute
# RETURN: Result of the query
def db_query(db_connection, query):
    cur = db_connection.cursor()
    query_result = []
    try:
        cur.execute(query)
        query_result = cur.fetchall()
    except MySQLdb.Error as error:
        print(error)
	
    return query_result
# Index
# Display product categories and their respective descriptions
@app.route('/')
def index():
    db_connection = get_connection()
    product_lines = db_query(db_connection,"SELECT productLine, textDescription FROM productlines")
    db_connection.close()

    return render_template('index.html', product_lines=product_lines)

# Product category page
# Display product name, category, and MSRP in a specific category
@app.route('/pages')
def pages():
    product_query = request.args.get('prodLine')
    db_connection = get_connection()
    product_desc = db_query(db_connection,"SELECT * FROM products WHERE productLine='" + product_query + "'" )
    db_connection.close()
    
    return render_template('pages.html',product_desc=product_desc)

# Product page
# Display product name, description, and MSRP
@app.route('/items')
def items():
    product_id = request.args.get('id')
    db_connection = get_connection()
    product_reviews = db_query(db_connection,"SELECT * FROM reviews WHERE productCode='" + product_id + "'") 
    product_desc = db_query(db_connection,"SELECT * FROM products WHERE productCode='" + product_id + "'" )
    db_connection.close()

    return render_template('items.html', product_id=product_id, product_desc=product_desc, product_reviews=product_reviews)

# Handles review submissions
@app.route('/review')
def review():
    name_review = request.args.get('person')
    desc_review = request.args.get('txt')
    product_id = request.args.get('prod')
    db_connection = get_connection()
    err = None
    try:
        cur = db_connection.cursor()
        cur.execute("INSERT INTO reviews VALUES(" + "'" + name_review + "'" + "," +  "'" +desc_review + "'"  + "," + "'" + product_id + "'" + ")")
        db_connection.commit()
    except MySQLdb.Error as error:
        err = error
        return render_template('index.html',err=err, desc_review=desc_review)

    db_connection.close()

    return redirect("items?id=%s"%product_id)

# Handles search results
@app.route('/results')
def results():
    search_term = "%" + request.args.get('keywords') + "%"
    db_connection = get_connection()
    db = MySQLdb.connect(host='localhost', user='root', passwd='compx519', db='db_compx519')
    product_desc = db_query(db_connection,"SELECT * FROM products WHERE productDescription LIKE '" + search_term + "'")
    db_connection.close()
    
    return render_template('results.html',product_desc=product_desc)

if __name__ == "__main__": 
    app.run() 

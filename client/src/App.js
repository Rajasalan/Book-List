import "./App.css";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import CreateBook from "./components/CreateBook";
import BookList from "./components/BookList";
import EditBook from "./components/EditBook";

function App(props) {
  return (
    <div className="App">
      <Router>
        <div className="container">
          <nav
            className="btn btn-warning navbar navheader"
            style={{ marginTop: "80px", paddingLeft: "300px" }}
          >
            <nav>
              <h1 style={{ textAlign: "center" }}> Garland's Book Collection</h1>
              <nav className="navbar-nav">
                <nav className="nav-item">
                  <button className="buttonNav">
                    <Link to={"/CreateBook"} className="nav-link">
                      Add Book
                    </Link>
                  </button>
                </nav>
                <li className="nav-item">
                    <Link to={"/BookList"} className="nav-link" style={{color:'black'}}>
                    <i className="fa fa-home fa-3x" aria-hidden="true"></i>
                    </Link>
                </li>
              </nav>
            </nav>
          </nav>

          <br />
          <Switch>
            <Route
              path="/CreateBook"
              render={(props) => <CreateBook {...props} />}
            />
            <Route
              path="/edit/:id"
              render={(props) => <EditBook {...props} />}
            />
           <Route exact path="/" component={BookList} render={(props) => <BookList {...props} />} />
           <Route
              path="/BookList"
              render={(props) => <BookList {...props} />}
            />
          </Switch>
        </div>
      </Router>
    </div>
  );
}

export default App;
